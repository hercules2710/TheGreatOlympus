using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrainController : MonoBehaviour
{
    public float activeRange = 80f;
    public float movingSpeed = 1f;
    public bool isActive = false;
    public GameObject stopTrigger;
    public GameObject destoyEffect;
    public GameObject[] spawnZone;
    GameObject Player;
    ObjectTaget objectTaget;
    int zone = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        objectTaget = GetComponent<ObjectTaget>();

    }

    // Update is called once per frame
    void Update()
    {
       if(isActive == false)
        {

            spawnZone[zone].SetActive(false);
            if (Vector3.Distance(transform.position, Player.transform.position) < activeRange)
            {
                transform.position = Vector3.Lerp(transform.position, stopTrigger.transform.position, movingSpeed * Time.deltaTime);
            }
            if(zone < spawnZone.Length-1)
            {
                zone++;
            }
        }
       else
        {
            gameObject.GetComponent<OpenGate>().isOpenGate = true;
            spawnZone[zone].SetActive(true);
            if (zone < spawnZone.Length-1)
            {
                zone++;
            }
            if(objectTaget.health < 0)
            {
                destoyEffect.SetActive(true);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, activeRange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("enemyTrainTrigger"))
        {
            isActive = true;
            zone = 0;
        }
    }
}
