using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billBoardScript : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camPlayer;
    void Start()
    {
        camPlayer = GameObject.FindGameObjectWithTag("camPlayer").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + camPlayer.forward);
    }
}
