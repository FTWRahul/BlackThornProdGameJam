using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float fltSpeed;
    public bool blnDead;

    public GameManager gameMng;

    // Update is called once per frame
    void Update() {
        if (blnDead) {
            fltSpeed = 0;
        } else {
            transform.position = Vector3.MoveTowards(gameObject.transform.position,
                                                 gameMng.objPlanet[gameMng.intCurrentPlanetIndex].transform.position,
                                                 fltSpeed * Time.deltaTime);
        }
    }
}
