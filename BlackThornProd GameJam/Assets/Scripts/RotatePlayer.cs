using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {

    /* Variables */
    public float fltAngularSpeed;
    public float fltLinearSpeed; 
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
    public LineRenderer lr;
    public Renderer rend;

    public AudioSource shootAudio;
    //public AudioSource betweenPlanetAudio;
    //public AudioSource rotatePlanetAudio;
    public GameObject rotateAudio;
    public GameObject betweenPlanetAudio;

    //Debugging and level help
    public bool drawRay;
    public float rayDurition;


    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        gameMng = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update() {
        // Make the player rotate with the horizontal movement keys
        if(!blnMovingBetweenPlanets)
        {
            betweenPlanetAudio.SetActive(false);

            transform.RotateAround(gameMng.objPlanet[gameMng.intCurrentPlanetIndex].transform.position, Vector3.back, Input.GetAxis("Horizontal") * fltAngularSpeed * Time.deltaTime);
            SetAnimations();

        } else { // Check distance between player and hit point
            if (Vector3.Magnitude(transform.position - new Vector3(hit.point.x, hit.point.y, 0f)) < gameMng.fltPlayerDistPlanet) {
                Debug.Log("HIT");
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
        if(hit)
        {
            //Checks if the ray hits obj tagged Planet
            if (hit.collider.CompareTag("Planet"))
            {
                if(drawRay)
                {
                    Debug.DrawRay(playerTip.transform.position, new Vector3(hit.point.x, hit.point.y, 0) - playerTip.transform.position, Color.green, rayDurition);
                }

                    //Checking for player input
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log(hit.point);

                    //changes the bool for the selected planet
                    hit.collider.gameObject.GetComponent<Planet>().blnTarget = true;
                    gameMng.FindTarget();
                    blnMovingBetweenPlanets = true;
                }
            }
        }
        
        if(blnMovingBetweenPlanets)
        {
            MoveToPlanet();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            ShootBullet();
        }
    }

    //Moving the player between 2 planets
    void MoveToPlanet()
    {
        betweenPlanetAudio.SetActive(true);
        anim.SetBool("BetweenPlanet", true);
        anim.SetBool("Idle", false);


        transform.position = Vector3.MoveTowards(transform.position, hit.point, fltLinearSpeed * Time.deltaTime);
    }

    //Spawns and shoots a bullet from the tip of the ship
    void ShootBullet()
    {
        shootAudio.Play();
        Instantiate(bullet, playerTip.transform.position, Player.transform.rotation);
    }

    void SetAnimations()
    {
        if (Input.GetAxisRaw("AnimHorz") == 0)
        {
            rotateAudio.SetActive(false);

            anim.SetBool("Idle", true);
            anim.SetBool("LeftThruster", false);
            anim.SetBool("RightThruster", false);
        }
        else if (Input.GetAxisRaw("AnimHorz") > 0)
        {
            rotateAudio.SetActive(true);

            anim.SetBool("Idle", false);
            anim.SetBool("RightThruster", false);

            //if (!anim.GetBool("LeftThruster"))
            //{
            //    anim.SetBool("LeftTran", true);
            anim.SetBool("LeftThruster", true);
            //}
            //else
            //{
            //    anim.SetBool("LeftTran", false);
            //}
        }
        else if (Input.GetAxisRaw("AnimHorz") < 0)

        {
            rotateAudio.SetActive(true);

            anim.SetBool("Idle", false);
            anim.SetBool("LeftThruster", false);

            //    if (!anim.GetBool("RightThruster"))
            //    {
            //        anim.SetBool("RightTran", true);
            anim.SetBool("RightThruster", true);
            //    }
            //    else
            //    {
            //        anim.SetBool("RightTran", false);
            //    }
        }
    }
}
