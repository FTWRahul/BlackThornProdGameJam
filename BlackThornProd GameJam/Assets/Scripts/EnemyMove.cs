using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float fltSpeed;
    public bool blnDead;
    public bool blnKilled;
    public int intHealth;

    public GameManager gameMng;

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
                                                 gameMng.objPlanet[gameMng.intCurrentPlanetIndex].transform.position,
                                                 fltSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        if(blnKilled)
        {
            gameMng.IncreaseScore();
        }
    }
}
