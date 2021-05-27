using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBoss : MonoBehaviour
{
    #region singleton
    public static SpiderBoss Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public GameObject target;
    public GameObject web;
    public GameObject Poision;
    public GameObject spawnLocation;
    public GameObject shield;
    public GameObject healthBar;
    public GameObject exitGate;
    public Animator animator;
    public float lookZone;
    public float attackRange;
    public float bossSpeed;
    public float Danmge;
    public bool isShieldable = false;
    public bool shielding = false;
    GameObject checkShootWebPoint;
    GameObject checkShootPoisionPoint;
    GameObject checkShieldPoint;   
    AudioSource audioS;
    Target targetScript;
    float dist;
    float realTime;
    float shieldTime = 10f;
    int atkMin = 0;
    int atkMax = 2;
    bool isRising = false;
    bool isAwake = false;

    // Start is called before the first frame update
    void Start()
    {
        checkShootPoisionPoint = GameObject.FindGameObjectWithTag("shootPoisionPoint");
        checkShootWebPoint = GameObject.FindGameObjectWithTag("shootWebPoint");
        checkShieldPoint = GameObject.FindGameObjectWithTag("ShiedldPoint");
        target = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        targetScript = GetComponent<Target>();
        audioS = GetComponent<AudioSource>();
        TimeUpdate();
        exitGate.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InAction();
    }
    void InAction()
    {
        if(targetScript.health <=0)
        {
            exitGate.SetActive(true);
        }
        if (animator != null)
        {
            dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
           if(isRising)
            {
                
                if(isAwake ==false)
                {
                    animator.SetFloat("Action", 0.5f);
                    animator.SetFloat("Action", 1f);
                    BosLevelController.Instance.PlayAudio();
                    isAwake = true;
                }
                else
                {
                    if (targetScript.health < 5000)
                    {
                        
                        if (isShieldable)
                        {
                            shielding = true;
                            if(targetScript.health >0)
                            {
                                OpenShield();
                            }                           
                        }   
                        if(shielding)
                        {
                            animator.SetFloat("Action", 1f);
                            audioS.Play();
                            SpiderComeOut();
                        }
                    }
                    if(shielding == false)
                    {
                        LookRotation();
                        audioS.Stop();
                        animator.SetFloat("Action", 2f);
                        spawnLocation.SetActive(false);
                        if(Time.time > realTime+shieldTime)
                        {
                            isShieldable = true;
                        } 
                    }
                    
                }
            }
           else
            {
                if (dist < lookZone)
                {
                    isRising = true;
                    targetScript.isImortal = false;
                }
                else
                {
                    targetScript.isImortal = true;
                }
            }
        }
    }
    void LookRotation()
    {
        if (animator.GetBool("Dying") == false)
        {
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            Quaternion rotateDirection = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateDirection, Time.deltaTime * 5f);
        }
    }
    public void TimeUpdate()
    {
        realTime = Time.time;
    }
    public void randomAttack()
    {
        int ranAtk = Random.Range(atkMin, atkMax + 1);
        if (animator != null)
        {
            switch (ranAtk)
            {
                case 1: animator.SetTrigger("WebShoot"); break;
                case 2: animator.SetTrigger("PosionShoot"); break;
            }
            animator.SetFloat("Action", 2f);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookZone);
    }
    public void WebShooting()
    {
        Instantiate(web, checkShootWebPoint.transform.position, Quaternion.identity);
    }
    public void PoisionShooting()
    {
        Instantiate(Poision, checkShootPoisionPoint.transform.position, Quaternion.identity);
    }
    public void SpiderComeOut()
    {
        spawnLocation.SetActive(true);
    }
    public void SelfHeal()
    {
        targetScript.health += 200;
        healthBar.GetComponent<HealthBar>().setHealth(targetScript.health);
    }
    public void OpenShield()
    {
        Instantiate(shield, checkShieldPoint.transform.position, Quaternion.identity);
        isShieldable = false;
    }
}
