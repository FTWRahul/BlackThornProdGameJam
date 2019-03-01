using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftEnemySpawnner : MonoBehaviour
{
    public GameManager gameMng;
    public GameObject enemyType1;
    public GameObject enemyType2;
    private GameObject EnemyTemp; // Temporary reference for the instantiated enemy object

    public float fltMinSpawnTime;
    public float fltMaxSpawnTime;
    public float fltTimeBetweenSpawn;
    public float fltSpawnTime;

    public int intEnemyCount; // Variable counter
    public int intWave;
    public int intWaveCounter; 

    public bool blnEnemySpawnned = true;

    public List<int> arrEnemyTypes; // Array of types of enemies (with health values)
    public WaveOfEnemies[] arrWaves; // Array of waves of enemies

    public Animator anim;

    // Start is called before the first frame update
    public void Start()
    {
        blnEnemySpawnned = true;
        //Define random spawn time for the enemy to spawn from the rift
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);

        // Assign the Game Manager
        gameMng = FindObjectOfType<GameManager>();

        anim = GetComponent<Animator>();

        // Get the arrays of enemies
        arrWaves = GetComponentsInChildren<WaveOfEnemies>();
        intWaveCounter = arrWaves.Length;

        arrEnemyTypes = arrWaves[intWave].arrHealth;
        //Debug.Log(intWaveCounter);

        //for (int i = 0; i < arrEnemyTypes.Count; i++) {
        //    intEnemyCount += arrWaves[i].arrHealth.Count;
        //}
        intEnemyCount += arrEnemyTypes.Count;
        Debug.Log(intEnemyCount);
        gameMng.intEnemiesRemaining += intEnemyCount;
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
        anim.SetTrigger("Spawn");
        blnEnemySpawnned = true;
        intEnemyCount--;
        if(fltMaxSpawnTime > 2)
        {
            fltMaxSpawnTime--;
        }
        //Debug.Log(intEnemyCount);
        if (arrEnemyTypes[intEnemyCount] < 2) {
            EnemyTemp = Instantiate(enemyType1, transform.position, Quaternion.identity);
            EnemyTemp.GetComponent<EnemyMove>().intHealth = arrEnemyTypes[intEnemyCount];
        } else {
            EnemyTemp = Instantiate(enemyType2, transform.position, Quaternion.identity);
            EnemyTemp.GetComponent<EnemyMove>().intHealth = arrEnemyTypes[intEnemyCount];
        }
        
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);

        if(intEnemyCount < 1)
        {
            anim.SetTrigger("End");
        }
    }
}
