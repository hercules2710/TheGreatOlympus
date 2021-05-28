using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawFlat : MonoBehaviour
{
    public float zone = 0.5f;
    public GameObject Object;
    public float xPos;
    public float zPos;
    public float spawnDelayTime;
    public float timedelay;
    public float flatcount;
    public float countdelay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLocation());
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnLocation()
    {

        while (flatcount < 200)
        {
            xPos = Random.Range(90, 150);
            zPos = Random.Range(-30, 30);
            for (countdelay = 0; countdelay <= flatcount; countdelay += 3)
            {
                if (flatcount == countdelay)
                {
                    yield return new WaitForSeconds(timedelay);
                }
            }
            Instantiate(Object, new Vector3(xPos, 10, zPos), Quaternion.identity);
            yield return new WaitForSeconds(spawnDelayTime);
            flatcount += 1;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, zone);
    }
}
