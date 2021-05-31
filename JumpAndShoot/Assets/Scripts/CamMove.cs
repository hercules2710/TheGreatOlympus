using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform playerBody;
    public Transform gunControll;
    public float lookUpHeight;
   // public PhotonView photonView;
    float mouseSensitivity = 120f;
    float rotateX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camMoving();
    }
    void camMoving()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateX -= mouseY;
        rotateX = Mathf.Clamp(rotateX, -lookUpHeight, lookUpHeight);
        transformLocal(rotateX);
        transformRotate(mouseX);
      //  photonView.RPC("transformLocal", PhotonTargets.AllBuffered, rotateX);
      //  photonView.RPC("transformRotate", PhotonTargets.AllBuffered, mouseX);
    }

  //  [PunRPC]
    void transformLocal(float rotateX)
    {
        transform.localRotation = Quaternion.Euler(rotateX, 0f, 0f);
        gunControll.localRotation = Quaternion.Euler(rotateX, 0f, 0f);
    }
  //  [PunRPC]
    void transformRotate(float mouseX)
    {
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
