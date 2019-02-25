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
    public bool blnEnemySpawnned;

    // Start is called before the first frame update
    void Start()
    {
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);

    }

    // Update is called once per frame
    void Update()
    {
        if(fltEnemyCount > -1)
        {
            if (!blnEnemySpawnned)
            {
                SpawnEnemy();
            }
            else
            {
                fltTimeBetweenSpawn += Time.deltaTime;
                if (fltTimeBetweenSpawn > fltSpawnTime)
                {
                    fltTimeBetweenSpawn = 0;
                    blnEnemySpawnned = false;
                }
            }
        }
 
    }

    public void SpawnEnemy()
    {
        blnEnemySpawnned = true;
        fltEnemyCount--;
        if(fltMaxSpawnTime>2)
        {
            fltMaxSpawnTime--;
        }
        if (fltEnemyCount > -1)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
        fltSpawnTime = Random.Range(fltMinSpawnTime, fltMaxSpawnTime);
    }
}
