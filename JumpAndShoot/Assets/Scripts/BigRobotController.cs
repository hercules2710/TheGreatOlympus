using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigRobotController : MonoBehaviour
{
    #region singleton
    public static BigRobotController Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    public float Danmge;
    public float lookZone;
    public float attackRange;
    public Animator animator;
    public Collider hitBox1;
    public Collider hitBox2;
    float dist;
    float randSpeed;
    int attackMin = 0;
    int attackMax = 3;
    GameObject target;
    NavMeshAgent enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        randSpeed = Random.Range(15f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        robotMove();
        if (animator.GetBool("Dying"))
        {
            Destroy(hitBox1);
            Destroy(hitBox2);
        }
    }
    void robotMove()
    {
        if (animator != null)
        {
            dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
            // transform.LookAt(target.transform.position);
            LookRotation();
            if (dist < lookZone)
            {
                if (dist > attackRange)
                {
                    animator.SetFloat("Action", 1f);
                    enemy.speed = randSpeed;
                    enemy.SetDestination(target.transform.position);
                }
                else
                {
                    enemy.SetDestination(gameObject.transform.position);
                    animator.SetFloat("Action", 2f);
                }
            }
            else
            {
                animator.SetFloat("Action", 0f);
                enemy.SetDestination(gameObject.transform.position);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookZone);
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
    public void randomAttack()
    {
        int ranAtk = Random.Range(attackMin, attackMax + 1);
        if (animator != null)
        {
            switch (ranAtk)
            {
                case 1: animator.SetTrigger("AtkTwoHand"); break;
                case 2: animator.SetTrigger("AtkPunchUp"); break;
                case 3: animator.SetTrigger("AtkPunch"); break;
            }
            animator.SetFloat("Action", 2f);
        }
    }
}
