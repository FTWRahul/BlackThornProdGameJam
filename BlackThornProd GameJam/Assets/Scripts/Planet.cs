using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Planet : MonoBehaviour {

    // Settings for the planets

    //public string strPlanetName;
    public int intHealth;
    public bool blnDead;
    public bool blnCurrent;

    // Planet Constructor
    public Planet(int inHealth, bool inDead, bool inCurrent) {
        intHealth = inHealth;
        blnDead = inDead;
        blnCurrent = inCurrent;
    }
}
