using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftEnemySpawnner : MonoBehaviour
{

    public GameObject enemy;
    public float fltMinSpawnTime;
    public float fltMaxSpawnTime;
    public float fltEnemyCount;
    public float fltTimeBetweenSpawn;
    public float fltSpawnTime;
    public bool blnEnemySpawnned = true;

    // Start is called before the first frame update
    void Start()
    {
        //Define random spawn time for the enemy to spawn from the rift
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Check how many enemys are remaining
        if(fltEnemyCount > -1)
        {
            //Check if enemy was spawnned
            if (!blnEnemySpawnned)
            {
                SpawnEnemy();
            }
            else
            {
                //Increament the time bewteen spawns
                fltTimeBetweenSpawn += Time.deltaTime;
                //Check if the time that has passed is greater than the time the enemy should spawn
                if (fltTimeBetweenSpawn > fltSpawnTime)
                {
                    //Set value of time between spawn to 0 and spawn enemy
                    fltTimeBetweenSpawn = 0;
                    blnEnemySpawnned = false;
                }
            }
        }
    }

    /// Spawns the enemy: Decreases the max spawn time: Randomizes a new spawn time
    public void SpawnEnemy()
    {
        blnEnemySpawnned = true;
        fltEnemyCount--;
        if(fltMaxSpawnTime>2)
        {
            fltMaxSpawnTime--;
        }
        Instantiate(enemy, transform.position, Quaternion.identity);
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);
    }
}
