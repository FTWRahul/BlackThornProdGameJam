using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour {

    public float fltVerticalSpeed;
    private bool blnShot;

    public GameManager gameMng;
    public GameObject player;
    public GameObject bullet;

    // Update is called once per frame
    void Update() {
        if (blnShot) { // Move the bullet only if it is shot
            MoveBullet();
        } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) { // Shoot if you press Up
            blnShot = true;
        }
    }

    // Make the bullet move forward
    void MoveBullet() {
        transform.Translate(0,fltVerticalSpeed * Time.deltaTime, 0);
    }

    // Make bullet collide with the enemy
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            // Kill the Enemy and destroy both Enemy and Bullet
            Destroy(collision.gameObject);
            Destroy(bullet);

            // Increase score
        }
    }
}
