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

    // Detects collision of enemy to planet; decreases the planet's health; destroys enemy
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Planet")) {
            // Decrase health
            collision.gameObject.GetComponent<Planet>().intHealth--;

            Debug.Log(collision.gameObject.GetComponent<Planet>().intHealth);

            // Destroy enemy
            Destroy(gameObject, 3f);

            if (collision.gameObject.GetComponent<Planet>().intHealth < 1) {
                Debug.Log("GAME OVER");
            }
        }
    }
}
