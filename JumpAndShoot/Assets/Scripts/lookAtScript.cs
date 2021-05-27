using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtScript : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject canvasVision;
     GameObject camPlayer;
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(camPlayer != null)
        {
                photonView.RPC("lookTransform", PhotonTargets.All);
                //lookTransform();   
        }
        else
        {
            if (!photonView.isMine)
            {
                camPlayer = GameObject.FindGameObjectWithTag("camPlayer");
            }
            
        }
        
    }
    [PunRPC]
    void lookTransform()
    {
        if (!photonView.isMine)
        {
            canvasVision.transform.LookAt(camPlayer.transform.position);
        }
    }
}
