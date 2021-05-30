using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaExplosion : MonoBehaviour
{
    public GameObject lavaExplosionPrefab;
    public int timeDelaySpawn;
    public int exposionPerTime;
    public int timeBreak;
    public Vector3 spawnZone;
    bool isSpawning;
    float timeCountDown;
    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;
        timeCountDown = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeCountDown)
        {
            if (!isSpawning)
            {
                StartCoroutine(spawnLavaExp());
            }
        }
    }
    IEnumerator spawnLavaExp()
    {
        isSpawning = true;
        for(int i = 0;i<exposionPerTime;i++)
        {
            spawn();
            yield return new WaitForSeconds(timeDelaySpawn);
        }
        isSpawning = false;
        timeCountDown = Time.time + timeBreak;
        yield break;
    }
    void spawn()
    {
        float x = transform.position.x + Random.Range(-spawnZone.x/2, spawnZone.x/2);
        float z = transform.position.z + Random.Range(-spawnZone.z/2, spawnZone.z/2);
        Instantiate(lavaExplosionPrefab, new Vector3(x, transform.position.y, z),Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position,spawnZone);
    }
}
