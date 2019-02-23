using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour {

    public float fltSpeed;
    public float fltAngle;

   public Vector3 axis;

    public GameObject targetPlanet;
    public GameObject Player;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.RotateAround(targetPlanet.transform.position, new Vector3(0,0,-1), Input.GetAxis("Horizontal")* fltSpeed * Time.deltaTime);
    }
}
