using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float fltSpeed;

    public GameManager gameMng;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, gameMng.objPlanet[gameMng.intPlanetIndex].transform.position , fltSpeed * Time.deltaTime);
    }
}
