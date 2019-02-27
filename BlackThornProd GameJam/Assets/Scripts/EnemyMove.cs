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


    public Animator anim;
    public GameManager gameMng;

    private void Start()
    {
        intPlanetToKill = Random.Range(0, gameMng.objPlanet.Count);
        gameMng = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        transform.rotation = Quaternion.FromToRotation(Vector3.down, transform.position - gameMng.objPlanet[intPlanetToKill].transform.position);

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

    // Increase score and check if all aliens have been killed
    private void OnDestroy()
    {
        if(blnKilled)
        {
            gameMng.IncreaseScore();
        }
        gameMng.intEnemiesRemaining--;
        gameMng.CheckForWin();
    }
}
