using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public GameObject spiderClone;
    GameObject[] spawnZone;
    private float spawnTime = 0;
    private float lastSpawnTime = 0;
    private float spawnMinTime = 2f;
    private float spawnMaxTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        spawnZone = GameObject.FindGameObjectsWithTag("spawnZone");
        RandomTimeSpawn();
    }
    void RandomTimeSpawn()
    {
        lastSpawnTime = Time.time;
        spawnTime = Random.Range(spawnMinTime, spawnMaxTime + 1);
    }
    void Spawn()
    {
        int zone = Random.Range(0, spawnZone.Length);
        Instantiate(spiderClone, spawnZone[zone].transform.position, Quaternion.identity);
        RandomTimeSpawn();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lastSpawnTime + spawnTime)
        {
            Spawn();
        }
    }

}
