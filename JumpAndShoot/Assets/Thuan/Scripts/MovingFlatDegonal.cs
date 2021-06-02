﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatDegonal : MonoBehaviour
{
    public float changeSpeed, changeDirection, rangeMoveSide;
    Vector3 Move;
    float z;
    float x;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(-rangeMoveSide, rangeMoveSide);
        z = Random.Range(-rangeMoveSide, rangeMoveSide);
        speed = Random.Range(0.5f, changeSpeed);

        Move = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move.y += speed;
        Move.x += x;
        Move.z += z;
        transform.position = Vector3.Lerp(transform.position, Move, speed * Time.deltaTime);
    }
}