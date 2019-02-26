using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Planet : MonoBehaviour {

    // Settings for the planet
    public int intHealth;
    public int int1stAnimState;
    public int int2ndAnimState;
    public bool blnDead;
    public bool blnCurrent;
    public bool blnTarget;
    public Animator anim;

    // Reference for Game Manager
    public GameManager gameMng;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Planet Constructor
    public Planet(int inHealth, bool inDead, bool inCurrent, bool inTarget) {
        intHealth = inHealth;
        blnDead = inDead;
        blnCurrent = inCurrent;
    }

    // Detects collision of enemy to planet; decreases the planet's health; destroys enemy
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            // Decrase health
            intHealth--;
            if(intHealth < int2ndAnimState)
            {
                anim.SetTrigger("Damaged2");
            }
            else if(intHealth < int1stAnimState )
            {
                anim.SetTrigger("Damaged1");
            }

            Debug.Log(intHealth);
            
            // Game Over
            if (intHealth < 1) {
                Debug.Log("GAME OVER");
                gameMng.EndGame();
            }
            collision.gameObject.GetComponent<EnemyMove>().blnDead = true;
            collision.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.down, collision.gameObject.transform.position - transform.position);
            collision.GetComponent<EnemyMove>().anim.SetBool("PlanetHit", true);

            // Destroy enemy
            Destroy(collision.gameObject, gameMng.fltAnimaDestroyEnemy);
        }
    }
}
