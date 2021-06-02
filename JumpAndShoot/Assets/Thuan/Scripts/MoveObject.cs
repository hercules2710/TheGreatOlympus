using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveObject : MonoBehaviour
{
    public float timer = 1;
    NavMeshAgent nav;
    bool InCorountine;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!InCorountine)
        {
            StartCoroutine(DoSomeThing());
        }
    }

    Vector3 getnewRandoomPosition()
    {
        float x = Random.Range(90, -150);
        float z = Random.Range(-30, 30);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomeThing()
    {
        InCorountine = true;
        yield return new WaitForSeconds(timer);
        GetNewPath();
        InCorountine = false;
    }

    void GetNewPath()
    {
        nav.SetDestination(getnewRandoomPosition());
    }
}
