using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huhu : MonoBehaviour
{
    GameObject Player;
    Rigidbody rigi;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rigi = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigi.AddForce(Vector3.up*20,ForceMode.Impulse);
    }
}
