using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {

    /* Variables */
    public float fltSpeed;
    public float fltAngle;
    //public int intPlanetNum;

    public GameManager gameMng;

    //public GameObject[] targetPlanet;
    public GameObject Player;
    public GameObject bullet;

    public GameObject playerTip;
    public GameObject shootDirection;
    public float fltMoveDistance;
    public bool blnMovingBetweenPlanets;


    // Start is called before the first frame update
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {
        // Make the player rotate with the horizontal movement keys
        if(!blnMovingBetweenPlanets)
        {
            transform.RotateAround(gameMng.objPlanet[gameMng.intCurrentPlanetIndex].transform.position, Vector3.back, Input.GetAxis("Horizontal") * fltSpeed * Time.deltaTime);
        }

        //Casts a ray in a direction
        RaycastHit2D hit = Physics2D.Raycast(playerTip.transform.position, shootDirection.transform.position - playerTip.transform.position, fltMoveDistance);
        //Checks if the ray hits obj tagged Planet
        if(hit.collider.CompareTag("Planet"))
        {
            //Checking for player input
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //changes the bool for the selected planet
                hit.collider.gameObject.GetComponent<Planet>().blnTarget = true;
                //MoveToPlanet();
                blnMovingBetweenPlanets = true;
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

    //Moving the player between 2 panets
    void MoveToPlanet()
    {
        transform.position = Vector3.MoveTowards(transform.position, gameMng.objPlanet[gameMng.intTargetPlanetIndex].transform.position, 10 * Time.deltaTime );
    }

    //Spawns and shoots a bullet from the tip of the ship
    void ShootBullet()
    {
        Instantiate(bullet, playerTip.transform.position, Quaternion.identity);
    }
}
