using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour {

    public float fltVerticalSpeed;
    //private bool blnShot;

    public GameManager gameMng;
    public GameObject player;
    public GameObject bullet;

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        MoveBullet();
    }

    // Make the bullet move forward
    void MoveBullet() {
        transform.Translate(0,fltVerticalSpeed * Time.deltaTime, 0); // Move
        Destroy(gameObject, gameMng.fltDestroyBullet); // Destroy the bullet after some time
    }

    // Make bullet collide with the enemy
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {

            collision.gameObject.GetComponent<EnemyMove>().hitSound.Play();
            //Set animations for the bullet hit
            transform.rotation = Quaternion.FromToRotation(Vector3.down, transform.position - collision.gameObject.transform.position);
            anim.SetBool("Hit", true);
            Destroy(bullet, gameMng.fltAnimaDestroyBullet);

            // Kill the Enemy and destroy both Enemy and Bullet
            //collision.GetComponent<EnemyMove>().blnDead = true;
            collision.GetComponent<EnemyMove>().intHealth--;
            if(collision.GetComponent<EnemyMove>().intHealth == 0)
            {
                collision.GetComponent<EnemyMove>().blnKilled = true;
                //collision.gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.down, collision.gameObject.transform.position - collision.gameObject.transform.position);
                collision.GetComponent<EnemyMove>().anim.SetBool("Killed", true);
                Destroy(collision.gameObject, gameMng.fltAnimaDestroyEnemy);

            }
            fltVerticalSpeed = 0;

            // Increase score
        }
        else if(collision.gameObject.CompareTag("Planet"))
        {
            collision.gameObject.GetComponent<Planet>().hitSound.Play();

            //Set animations for the bullet hit
            transform.rotation = Quaternion.FromToRotation(Vector3.down, transform.position - collision.gameObject.transform.position);
            anim.SetBool("Hit", true);
            fltVerticalSpeed = 0;
            Destroy(bullet, gameMng.fltAnimaDestroyBullet);
        }
    }
}
