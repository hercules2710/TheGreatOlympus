using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBullet : MonoBehaviour
{
    public float speedBullet;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        target = EvaController.Instance.enemyTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.position = Vector3.Lerp(transform.position,target,speedBullet*Time.deltaTime);
    }
}
