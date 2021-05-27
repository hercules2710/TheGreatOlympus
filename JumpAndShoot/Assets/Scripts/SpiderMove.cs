using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMove : MonoBehaviour
{
    float damngeToPlayer = 5f;
    public float attackRange; 
    GameObject Target;
    GameObject gun;
    public float moveSpeed;
    private float minSpeed = 0.2f;
    private float maxSpeed = 0.3f;
  //  Animator Animator;
    
    // Start is called before the first frame update
    void Start()
    {
        RandomSpeed();       
        Target = GameObject.FindGameObjectWithTag("Player");
        gun = GameObject.FindGameObjectWithTag("Gun");
      //  Animator.GetComponent<Animator>();
    }
    void RandomSpeed()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed + 0.5f);
    }
    // Update is called once per frame
    void Update()
    {      
        spiderMove();
    }
    void spiderMove()
    {
        if (Target == null)
        {
            return;
        }
        transform.LookAt(Target.transform.position);
        if(Vector3.Distance(Target.transform.position,gameObject.transform.position) > attackRange)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, moveSpeed * Time.deltaTime);
        }       
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.transform.tag.Equals("Player"))
    //    {
    //        //Animator.SetBool("Attack", true);
            
    //    }
    //}
    public void SpiderAttack()
    {
        gun.GetComponent<PlayerShoot>().PlayerTakeDamnge(damngeToPlayer);
    }
}
