using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOfEnemies : MonoBehaviour {

    public List<int> arrHealth;

    void DestroySelf() {
        Destroy(gameObject);
    }
}
