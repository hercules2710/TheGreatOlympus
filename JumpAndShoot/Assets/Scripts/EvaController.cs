using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvaController : MonoBehaviour
{
    #region singleton
    public static EvaController Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public GameObject bullet;
    public GameObject enemyTarget;
    public GameObject shootPoint;
    public float standDistance;
    public float danmge;
    public float speed;
    public float range;
    NavMeshAgent agent;
    GameObject player;
    GameObject[] Enemies;
    float fireCountDown = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("searchEnemy", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
        if(enemyTarget!=null)
        {
            fireCountDown -= Time.deltaTime;
            if(fireCountDown <=0)
            {              
                Shoot();
                fireCountDown = 0.3f;
            }
            transform.LookAt(enemyTarget.transform.position);           
        }
    }
    void followPlayer()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist > standDistance)
        {
            agent.speed = speed;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.speed = 0;
        }
        
    }
    void searchEnemy()
    {
        string[] tags = { "Enemy" };//, "standByEnemy" };
        foreach(string tag in tags)
        {
            Enemies = GameObject.FindGameObjectsWithTag(tag);
        }
        float distNearest = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in Enemies)
        {
            float distEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if(distEnemy<distNearest)
            {
                distNearest = distEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && distNearest<= range)
        {
            enemyTarget = nearestEnemy;
        }
        else
        {
            enemyTarget = null;
        }
        
    }
    void Shoot()
    {
        Instantiate(bullet,shootPoint.transform.position,Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
