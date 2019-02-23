using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Planet : MonoBehaviour {

    //public string strPlanetName;
    public int intHealth;
    public bool blnDead;

    public Planet(int inHealth, bool inDead) {
        intHealth = inHealth;
        blnDead = inDead;
    }
}
