//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatX : MonoBehaviour
{
    public GameObject Player;
    public float speed = 0.05f, changeDirection = -1, rangeMoveSide;
    Vector3 Move;
    float z;
    float x;
    // Start is called before the first frame update
    void Start()
    {
         x = Random.Range(-rangeMoveSide, rangeMoveSide);
         z = Random.Range(-rangeMoveSide, rangeMoveSide);
        
        Move = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Move.y += speed;
        Move.x += x;
        Move.z += z;
        transform.position =  Vector3.Lerp(transform.position,Move,speed*Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }
}
