﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatX : MonoBehaviour
{
    // public GameObject Player;
    public float changeSpeed , changeDirection, rangeMoveSide;
    Vector3 Move;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.5f,changeSpeed);
        
        Move = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
            Move.y += speed;
            transform.position = Vector3.Lerp(transform.position, Move, speed * Time.deltaTime);
    }
  /*  private void OnTriggerEnter(Collider other)
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
    }*/
}