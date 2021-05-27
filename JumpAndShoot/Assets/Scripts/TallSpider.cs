using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallSpider : MonoBehaviour
{

    #region singleton
    public static TallSpider Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion
    GameObject Target;
    GameObject Look;
    public GameObject web;
    public float moveSpeed;
    public float attackRange;
    private float minSpeed = 0.1f;
    private float maxSpeed = 0.15f;
    public float realTime;
    public float Danmge = 10f;
    public Animator tallSpiderTranferAttack;
    //  Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        RandomSpeed();
        Target = GameObject.FindGameObjectWithTag("Player");
        Look = GameObject.FindGameObjectWithTag("Player");
        //gun = GameObject.FindGameObjectWithTag("Gun");
        //  Animator.GetComponent<Animator>();
    }
    void RandomSpeed()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed + 0.5f) - 0.1f;
    }
    // Update is called once per frame
    void Update()
    {

        spiderMove();
    }
    void spiderMove()
    {
        if (Target == null || Look == null)
        {
            return;
        }
        transform.LookAt(Look.transform.position);
        if (Vector3.Distance(Target.transform.position, gameObject.transform.position) > attackRange)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = gameObject.transform.position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            //Animator.SetBool("Attack", true);

        }
    }
    public void SpiderAttack()
    {
        Instantiate(web, gameObject.transform.position, Quaternion.identity);
        //realTime = Time.time;
        tallSpiderTranferAttack.SetBool("Attack", false);
    }
}
