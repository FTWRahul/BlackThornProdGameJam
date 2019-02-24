using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Planet : MonoBehaviour {

    // Settings for the planet
    public int intHealth;
    public bool blnDead;
    public bool blnCurrent;

    // Reference for Game Manager
    public GameManager gameMng;

    // Planet Constructor
    public Planet(int inHealth, bool inDead, bool inCurrent) {
        intHealth = inHealth;
        blnDead = inDead;
        blnCurrent = inCurrent;
    }

    // Detects collision of enemy to planet; decreases the planet's health; destroys enemy
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            // Decrase health
            intHealth--;

            Debug.Log(intHealth);

            // Game Over
            if (intHealth < 1) {
                Debug.Log("GAME OVER");
                gameMng.EndGame();
            }

            // Destroy enemy
            Destroy(collision.gameObject, 3f);
        }
    }
}
