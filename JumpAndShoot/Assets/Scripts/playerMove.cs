using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    //public float maxVel;

    public CharacterController controller;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
       // player = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        playerController();
    }
    void playerController()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0f, v).normalized;
        //player.AddForce(move);
        if(move.magnitude > 0.1f)
        {
            float rollAngel = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
          //  float smoothAgel = Mathf.SmoothDampAngle(transform.eulerAngles.x, rollAngel, ref turnSmoothVel, smoothTime);
            transform.rotation = Quaternion.Euler(0f, rollAngel, 0f);
            controller.Move(move * speed * Time.deltaTime);
        }
    }
}
