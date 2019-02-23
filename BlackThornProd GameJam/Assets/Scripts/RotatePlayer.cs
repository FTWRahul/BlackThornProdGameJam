using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {

    public float fltSpeed;
    public float fltAngle;
    public int intPlanetNum;

    public GameObject[] targetPlanet;
    public GameObject Player;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.RotateAround(targetPlanet[intPlanetNum].transform.position, Vector3.back, Input.GetAxis("Horizontal")* fltSpeed * Time.deltaTime);
    }
}
