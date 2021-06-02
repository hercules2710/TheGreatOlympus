using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawFlat : MonoBehaviour
{
    public GameObject[] Flat;
    public float zone;
    public float spawnDelayTime;
    public float flatSpawnPosY;
    public float spawnMos;
    public float spawnLeas;
    public bool stop;

    float xPos;
    float zPos;
    int randFlat;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLocation());
    }
    // Update is called once per frame
    void Update()
    {
        spawnDelayTime = Random.Range(spawnLeas,spawnMos);
    }
    IEnumerator SpawnLocation()
    {
        xPos = transform.position.x + Random.Range(-zone / 2, zone / 2);
        zPos = transform.position.y + Random.Range(-zone / 2, zone / 2);
        while (!stop)
        {
            randFlat = Random.Range(0, 2);
            Vector3 spawnPosition = new Vector3(Random.Range(-xPos, xPos), flatSpawnPosY, Random.Range(-zPos, zPos));
            Instantiate(Flat[randFlat], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, zone);
    }
}
