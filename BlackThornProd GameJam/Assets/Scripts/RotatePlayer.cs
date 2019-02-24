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

    //public GameObject[] targetPlanet;
    public GameObject Player;
    public GameObject bullet;

    public GameObject playerTip;
    public GameObject shootDirection;
    public float fltMoveDistance;
    public bool blnMovingBetweenPlanets;

    private RaycastHit2D hit;
    //private Vector3 shootVector;


    // Start is called before the first frame update
    void Start() {
        //shootVector = shootDirection.transform.position - playerTip.transform.position;
    }

    // Update is called once per frame
    void Update() {
        // Make the player rotate with the horizontal movement keys
        if(!blnMovingBetweenPlanets)
        {
            transform.RotateAround(gameMng.objPlanet[gameMng.intCurrentPlanetIndex].transform.position, Vector3.back, Input.GetAxis("Horizontal") * fltAngularSpeed * Time.deltaTime);
        } else { // Check distance between player and hit point
            if (Vector3.Magnitude(transform.position - new Vector3(hit.point.x, hit.point.y, 0f)) < 0.8f) {
                blnMovingBetweenPlanets = false;
                gameMng.objPlanet[gameMng.intCurrentPlanetIndex].blnCurrent = false;
                gameMng.objPlanet[gameMng.intTargetPlanetIndex].blnTarget = false;
                gameMng.objPlanet[gameMng.intTargetPlanetIndex].blnCurrent = true;
                gameMng.intCurrentPlanetIndex = gameMng.intTargetPlanetIndex;
                // Rotate the player to be aligned with the planet's surface
                transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.position - gameMng.objPlanet[gameMng.intTargetPlanetIndex].transform.position);
            }
        }

        hit = Physics2D.Raycast(playerTip.transform.position, shootDirection.transform.position - playerTip.transform.position, fltMoveDistance);
        //Casts a ray in a direction
        //if(Physics2D.Raycast(playerTip.transform.position, shootDirection.transform.position - playerTip.transform.position, fltMoveDistance), out hit)
        //if (Physics2D.Raycast(playerTip.transform.position, shootDirection.transform.position - playerTip.transform.position, fltMoveDistance))
        if(hit)
        {     
            //Checks if the ray hits obj tagged Planet
            if (hit.collider.CompareTag("Planet"))
            {
                //Checking for player input
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log(hit.point);

                    //changes the bool for the selected planet
                    hit.collider.gameObject.GetComponent<Planet>().blnTarget = true;
                    //MoveToPlanet();
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
        transform.position = Vector3.MoveTowards(transform.position, hit.point, fltLinearSpeed * Time.deltaTime);
    }

    //Spawns and shoots a bullet from the tip of the ship
    void ShootBullet()
    {
        Instantiate(bullet, playerTip.transform.position, Player.transform.rotation);
    }
}
