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
    public Transform objWave;

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
        //objWave = this.gameObject.transform.GetChild(0);
        //intWaveCounter = arrWaves.Length;

        if (arrWaves.Length < 1) {
        //if (objWave == null) {
            //Debug.Log("NULL");
            //Destroy(gameObject); // Destroy self
            //gameMng.CheckForWin();
        } else {
            // Get the first wave
            objWave = this.gameObject.transform.GetChild(0);
            arrEnemyTypes = arrWaves[0].arrHealth;
            intEnemyCount += arrEnemyTypes.Count;
            gameMng.intEnemiesRemaining += intEnemyCount; // Total counter for the Gama Manager
        }
        
        
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
        //Debug.Log(intEnemyCount);
        if (arrEnemyTypes[intEnemyCount] < 2) {
            EnemyTemp = Instantiate(enemyType1, transform.position, Quaternion.identity);
            EnemyTemp.GetComponent<EnemyMove>().intHealth = arrEnemyTypes[intEnemyCount];
        } else {
            EnemyTemp = Instantiate(enemyType2, transform.position, Quaternion.identity);
            EnemyTemp.GetComponent<EnemyMove>().intHealth = arrEnemyTypes[intEnemyCount];
        }
        
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);

        if(intEnemyCount < 1) {
            Destroy(objWave.gameObject);
            //arrWaves = [];
            //arrWaves = GetComponentsInChildren<WaveOfEnemies>();
            Debug.Log("LENGTH:" + GetComponentsInChildren<WaveOfEnemies>().Length);
            //Debug.Log("LENGTH:" + GetComponentsInChildren<WaveOfEnemies>());
            if (GetComponentsInChildren<WaveOfEnemies>().Length < 2) {
                
                anim.SetTrigger("End"); // Animation of ending spawner
                Destroy(gameObject, 0.8f); // Destroy self
                StartCoroutine(LateCall());
            }
        }
    }

    // Wait for 1 second to check for win, but keep running the code
    IEnumerator LateCall() {
        yield return new WaitForSeconds(1);
        gameMng.CheckForWin();
    }
}
