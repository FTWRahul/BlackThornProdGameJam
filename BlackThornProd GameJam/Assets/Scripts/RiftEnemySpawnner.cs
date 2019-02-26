using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftEnemySpawnner : MonoBehaviour
{
    public GameManager gameMng;
    public GameObject enemy;
    private GameObject EnemyTemp; // Temporary reference for the intantiated enemy object
    public float fltMinSpawnTime;
    public float fltMaxSpawnTime;
    public float fltTimeBetweenSpawn;
    public float fltSpawnTime;
    public int intEnemyCount;
    public bool blnEnemySpawnned = true;
    public List<int> arrEnemyTypes; // Array of types of enemies (with health values)

    // Start is called before the first frame update
    void Start()
    {
        //Define random spawn time for the enemy to spawn from the rift
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);

        // Assign the Game Manager and 
        gameMng = FindObjectOfType<GameManager>();
        intEnemyCount = arrEnemyTypes.Count;
    }

    // Update is called once per frame
    void Update()
    {
        //Check how many enemys are remaining
        if(intEnemyCount > 0)
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
        intEnemyCount--;
        if(fltMaxSpawnTime > 2)
        {
            fltMaxSpawnTime--;
        }
        Debug.Log(intEnemyCount);
        EnemyTemp = Instantiate(enemy, transform.position, Quaternion.identity);
        EnemyTemp.GetComponent<EnemyMove>().intHealth = arrEnemyTypes[intEnemyCount];
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);
    }
}
