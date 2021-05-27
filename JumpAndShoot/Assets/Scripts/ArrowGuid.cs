using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGuid : MonoBehaviour
{
    public GameObject Gate;
    // Start is called before the first frame update
    void Start()
    {
        Gate = GameObject.FindGameObjectWithTag("ExitGate");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Gate.transform.position);
    }
}
