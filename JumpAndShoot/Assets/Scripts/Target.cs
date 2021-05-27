using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Target : MonoBehaviour
{
    public float health;
    public float dyingTime;
    public bool isImortal = false;
    Animator DyingAnim;
    GameObject Gun;
    public Collider enemyCollider;
    Rigidbody rigi;
    public GameObject healthBar;
    bool isDead = false;
    NavMeshAgent enemy;
    //  float realTime;
    //float animDelayTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        DyingAnim = GetComponent<Animator>();
        Gun = GameObject.FindGameObjectWithTag("Gun");
        rigi = GetComponent<Rigidbody>();
        enemy = GetComponent<NavMeshAgent>();
        //healthBar = GameObject.FindGameObjectWithTag("healthBar");
        healthBar.GetComponent<HealthBar>().setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            if(enemy!= null)
            {
                enemy.acceleration = 0f;
               // enemy.enabled = false;
            }
        }
    }
    public void TakeDamnge(float damnge)
    {
        //realTime = Time.time;
        if(isImortal == false)
        {
            health -= damnge;
            healthBar.GetComponent<HealthBar>().setHealth(health);
            if (health <= 0 && !isDead)
            {
                DyingAnim.SetTrigger("Dying");
                Destroy(enemyCollider);
                Gun.GetComponent<PlayerShoot>().GetPoint();
                isDead = true;
                Destroy(gameObject, dyingTime);
                if (rigi != null)
                {
                    rigi.useGravity = false;
                }
            }          
        }
        
    }
}
