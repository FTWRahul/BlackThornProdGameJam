using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float fltSpeed;
    public bool blnDead;
    public bool blnKilled;
    public int intHealth;

    public AudioSource hitSound;

    //public int intEnemyType;

    public int intPlanetToKill;
    //public int intNewPlanetToKill;
    public int intOldPlanetToKill;



    public Animator anim;
    public GameManager gameMng;

    private void Start()
    {
        gameMng = FindObjectOfType<GameManager>();
        //RandomizePlanet();
        do
        {
            intPlanetToKill = Random.Range(0, gameMng.objPlanet.Count);
            transform.rotation = Quaternion.FromToRotation(Vector3.down, transform.position - gameMng.objPlanet[intPlanetToKill].transform.position);
        }
        while (gameMng.objPlanet[intPlanetToKill].blnDead);
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {

        if(intHealth < 1)
        {
            blnDead = true;
        }
        if (blnDead) {
            fltSpeed = 0;
        } else {
            transform.position = Vector3.MoveTowards(gameObject.transform.position,
                                                 gameMng.objPlanet[intPlanetToKill].transform.position,
                                                 fltSpeed * Time.deltaTime);
        }
    }

    public void RandomizePlanet()
    {
        intPlanetToKill = Random.Range(0, gameMng.objPlanet.Count);
        transform.rotation = Quaternion.FromToRotation(Vector3.down, transform.position - gameMng.objPlanet[intPlanetToKill].transform.position);
    }

    // Increase score and check if all aliens have been killed
    private void OnDestroy()
    {
        //Debug.Log("KILLED ON IMPACT");
        if(blnKilled)
        {
            gameMng.IncreaseScore();
        }
        gameMng.intEnemiesRemaining--;
        gameMng.CheckForWin();
    }
}
