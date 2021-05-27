using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletOnline : MonoBehaviour
{
    public int damnge;
    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!photonView.isMine) return;
        {
            if(collision.transform.tag.Equals("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().takeDamnge(damnge);
            }
        }
    }
}
