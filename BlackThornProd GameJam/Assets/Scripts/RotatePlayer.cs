using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {

    /* Variables */
    public float fltAngularSpeed;
    public float fltLinearSpeed;
    public float fltTimeBetweenShots;
    private bool blnBulletShot;
    public float fltBulletSpawnTime;
    //public float fltAngle;
    //public int intPlanetNum;

    public GameManager gameMng;
    public Animator anim;

    //public GameObject[] targetPlanet;
    public GameObject Player;
    public GameObject bullet;

    public GameObject playerTip;
    public GameObject shootDirection;
    public float fltRayCastDistance;
    public bool blnMovingBetweenPlanets;

    private RaycastHit2D hit;
    private Vector3 tempHit;
    public LineRenderer lr;
    public Renderer rend;
    private Vector3[] lrPositions;

    public AudioSource shootAudio;
    //public AudioSource betweenPlanetAudio;
    //public AudioSource rotatePlanetAudio;
    public GameObject rotateAudio;
    public GameObject betweenPlanetAudio;
    public GameObject idleAudio;

    //Debugging and level help
    public bool drawRay;
    public float rayDurition;


    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        gameMng = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Make the player rotate with the horizontal movement keys
        if (!blnMovingBetweenPlanets)
        {
            lrPositions = new Vector3[0];
            lr.positionCount = lrPositions.Length;
            lr.SetPositions(lrPositions);
            betweenPlanetAudio.SetActive(false);

            transform.RotateAround(gameMng.objPlanet[gameMng.intCurrentPlanetIndex].transform.position, Vector3.back, Input.GetAxis("Horizontal") * fltAngularSpeed * Time.deltaTime);
            SetAnimations();

        }
        else
        { // Check distance between player and hit point
            if (Vector3.Magnitude(transform.position - tempHit) < gameMng.fltPlayerDistPlanet)
            {

                // Manage indices for target and current planet
                blnMovingBetweenPlanets = false;
                gameMng.ManageTargets();
                anim.SetBool("BetweenPlanet", false);
                anim.SetBool("Idle", true);

                // Rotate the player to be aligned with the planet's surface
                transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.position - gameMng.objPlanet[gameMng.intTargetPlanetIndex].transform.position);
            }
        }


        hit = Physics2D.Raycast(playerTip.transform.position, shootDirection.transform.position - playerTip.transform.position, fltRayCastDistance);
        //Casts a ray in a direction
        if (hit)
        {
            if (drawRay)
            {
                Debug.DrawRay(playerTip.transform.position, new Vector3(hit.point.x, hit.point.y, 0) - playerTip.transform.position, Color.red, rayDurition);
            }

            //Checks if the ray hits obj tagged Planet
            if (hit.collider.CompareTag("Planet"))
            {
                if (drawRay)
                {
                    Debug.DrawRay(playerTip.transform.position, new Vector3(hit.point.x, hit.point.y, 0) - playerTip.transform.position, Color.green, rayDurition);
                }

                //if (Input.GetKey(KeyCode.S))
                if (Input.GetButton("Aim"))
                {
                    lrPositions = new Vector3[2];
                    lrPositions[0] = new Vector3(playerTip.transform.position.x, playerTip.transform.position.y, playerTip.transform.position.z);
                    lrPositions[1] = new Vector3(hit.point.x, hit.point.y, 0);
                    lr.positionCount = lrPositions.Length;
                    lr.SetPositions(lrPositions);
                    //Checking for player input                   
                }
                //if (Input.GetKeyDown(KeyCode.Space))
                if (Input.GetButtonDown("Fly"))
                {
                    tempHit = new Vector3(hit.point.x, hit.point.y, 0);
                    Debug.Log(tempHit);
                    //Debug.Log(hit.point);

                    //changes the bool for the selected planet
                    hit.collider.gameObject.GetComponent<Planet>().blnTarget = true;
                    gameMng.FindTarget();
                    blnMovingBetweenPlanets = true;
                }

            }

        }

        if (blnMovingBetweenPlanets)
        {
            MoveToPlanet();
        }

        if (!blnBulletShot)
        {
            //if (!blnMovingBetweenPlanets && (Input.GetKeyDown(KeyCode.W)))
            if (!blnMovingBetweenPlanets && Input.GetButtonDown("Shoot"))
            {
                ShootBullet();
            }
        }
        else
        {
            //Increament the time bewteen spawns
            fltTimeBetweenShots += Time.deltaTime;
            //Check if the time that has passed is greater than the time the enemy should spawn
            if (fltTimeBetweenShots > fltBulletSpawnTime)
            {
                //Set value of time between spawn to 0 and spawn enemy
                fltTimeBetweenShots = 0;
                blnBulletShot = false;
            }
        }
    }

    //Moving the player between 2 planets
    void MoveToPlanet()
    {
        betweenPlanetAudio.SetActive(true);
        anim.SetBool("BetweenPlanet", true);
        anim.SetBool("Idle", false);
        lrPositions = new Vector3[2];
        lrPositions[0] = new Vector3(playerTip.transform.position.x, playerTip.transform.position.y, playerTip.transform.position.z);
        lrPositions[1] = new Vector3(hit.point.x, hit.point.y, 0);
        lr.positionCount = lrPositions.Length;
        lr.SetPositions(lrPositions);


        transform.position = Vector3.MoveTowards(transform.position, tempHit, fltLinearSpeed * Time.deltaTime);
        Debug.Log("You are travelling to"+tempHit);

    }

    //Spawns and shoots a bullet from the tip of the ship
    void ShootBullet()
    {
        shootAudio.Play();
        blnBulletShot = true;
        Instantiate(bullet, playerTip.transform.position, Player.transform.rotation);
    }

    void SetAnimations()
    {
        if (Input.GetAxisRaw("AnimHorz") == 0)
        {
            idleAudio.SetActive(true);
            rotateAudio.SetActive(false);

            anim.SetBool("Idle", true);
            anim.SetBool("LeftThruster", false);
            anim.SetBool("RightThruster", false);
        }
        else if (Input.GetAxisRaw("AnimHorz") > 0)
        {
            idleAudio.SetActive(false);

            rotateAudio.SetActive(true);

            anim.SetBool("Idle", false);
            anim.SetBool("RightThruster", false);

            anim.SetBool("LeftThruster", true);
        }
        else if (Input.GetAxisRaw("AnimHorz") < 0)
        {
            idleAudio.SetActive(false);

            rotateAudio.SetActive(true);

            anim.SetBool("Idle", false);
            anim.SetBool("LeftThruster", false);

            anim.SetBool("RightThruster", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMove>().hitSound.Play();
            gameMng.IncreaseMultiplier();
            collision.GetComponent<EnemyMove>().blnKilled = true;
            collision.GetComponent<EnemyMove>().anim.SetBool("Killed", true);
            Destroy(collision.gameObject, gameMng.fltAnimaDestroyEnemy);
        }
    }
}
