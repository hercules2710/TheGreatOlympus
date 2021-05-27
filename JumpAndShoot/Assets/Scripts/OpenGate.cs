using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    public GameObject door1;
    public GameObject door2;
    public float openSpeed;
    public Vector3 door1OpenPosition;
    public Vector3 door2OpenPosition;
    public bool isOpenGate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpenGate)
        {
            door1.transform.position = Vector3.Lerp(door1.transform.position, door1OpenPosition, openSpeed * Time.deltaTime);
            door2.transform.position = Vector3.Lerp(door2.transform.position, door2OpenPosition, openSpeed * Time.deltaTime);
        }
    }
}
