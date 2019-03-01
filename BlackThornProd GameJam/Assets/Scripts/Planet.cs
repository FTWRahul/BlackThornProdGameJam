using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private Slider sliderHealth;
    public AudioSource hitSound;
    public int planetType;

    // Reference for Game Manager
    public GameManager gameMng;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gameMng = FindObjectOfType<GameManager>();

        // Set up the health bar
        sliderHealth = GetComponentInChildren<Slider>();
        sliderHealth.maxValue = intHealth;
        sliderHealth.value = sliderHealth.maxValue;
        sliderHealth.gameObject.SetActive(false);

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
            // Decrase health and show that in the health bar
            sliderHealth.gameObject.SetActive(true);
            intHealth--;
            sliderHealth.value--;
            hitSound.Play();

            if (intHealth < int2ndAnimState)
            {
                anim.SetTrigger("Damaged2");
            }
            else if(intHealth < int1stAnimState )
            {
                anim.SetTrigger("Damaged1");
            }

            Debug.Log(intHealth);
            
            // Game Over if any planet is destroyed
            if (intHealth < 1) {
                sliderHealth.gameObject.SetActive(false);
                Debug.Log("GAME OVER");
                gameMng.EndGame();
            }
            collision.gameObject.GetComponent<EnemyMove>().blnDead = true;
            collision.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.down, collision.gameObject.transform.position - transform.position);
            collision.GetComponent<EnemyMove>().anim.SetBool("PlanetHit", true);

            // Destroy enemy
            Destroy(collision.gameObject, gameMng.fltAnimaDestroyEnemy);

            // Disable the health bar after all the animations
            StartCoroutine(LateCall());
            }
    }

    IEnumerator LateCall() {
        yield return new WaitForSeconds(1);

        sliderHealth.gameObject.SetActive(false);
    }

    public void SetPlayerSpeeds()
    {
        if (planetType < 2 )
        {
            gameMng.player.fltAngularSpeed = gameMng.speedPlanetSmall;
        }
        else if (planetType < 3)
        {
            gameMng.player.fltAngularSpeed = gameMng.speedPlanetMed;
        }
        else if (planetType < 4)
        {
            gameMng.player.fltAngularSpeed = gameMng.speedPlanetLarge;
        }
        else
        {
            gameMng.player.fltAngularSpeed = gameMng.speedPlanetLarge;
        }
    }
}
