using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
    

public class RobotAutoController : MonoBehaviour
{
    #region Singleton
    public static RobotAutoController Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public float Danmge ;
    public float lookZone;
    public Animator animator;
    public Collider hitBox1;
    public Collider hitBox2;
    public Collider hitBox3;
    float attackRange = 2f;
    float jumpRange = 13f;
    float dist;
    float randSpeed;
    int attackMin = 0;
    int attackMax = 4; 
    GameObject target;
    NavMeshAgent enemy;

    // Start is called before the first frame update
    void Start()
    {
       // animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        randSpeed = Random.Range(9, 15+1);
    }

    // Update is called once per frame
    void Update()
    {
        robotMove();
        if(animator.GetBool("Dying"))
        {
            Destroy(hitBox1);
            Destroy(hitBox2);
            Destroy(hitBox3);
        }
    }
    void robotMove()
    {      
       if(animator != null)
        {
            dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
            // transform.LookAt(target.transform.position);
            LookRotation();
            if (dist < lookZone)
            {
                if (dist > jumpRange)
                {
                    animator.SetFloat("action", 1f);
                    enemy.speed = randSpeed;
                    enemy.SetDestination(target.transform.position);
                    //transform.position = Vector3.Lerp(transform.position,target.transform.position,moveSpeed * Time.deltaTime);
                }
                else if (dist > attackRange)
                {
                    animator.SetFloat("action", 1.5f);
                    enemy.SetDestination(target.transform.position);
                    enemy.speed = randSpeed + 8f;
                }
                else
                {
                    enemy.SetDestination(gameObject.transform.position);
                    // animator.SetTrigger("atkPunch");
                    animator.SetFloat("action", 2f);
                }
            }
            else
            {
                animator.SetFloat("action", 0f);
                enemy.SetDestination(gameObject.transform.position);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookZone);
    }
    public void randomAttack()
    {
        int ranAtk = Random.Range(attackMin, attackMax + 1);
       if(animator !=null)
        {
           switch(ranAtk)
            {
                case 1: animator.SetTrigger("atkPunch"); break;
                case 2: animator.SetTrigger("atkKick"); break;
                case 3: animator.SetTrigger("atkKock"); break;
                case 4: animator.SetTrigger("atkMulti"); break;
            }
            animator.SetFloat("action", 2f);
        }
    }
    void LookRotation()
    {
        if(animator.GetBool("Dying") == false)
        {
            Vector3 targetDirection = (target.transform.position - transform.position).normalized;
            Quaternion rotateDirection = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateDirection, Time.deltaTime * 5f);
        }
    }
   
}
