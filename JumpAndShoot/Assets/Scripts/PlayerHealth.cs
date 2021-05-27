using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject healthBar;
    public GameObject healthBarVision;
    public GameObject gameOverPanel;
    public ParticleSystem hitEffect;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<HealthBar>().setMaxHealth(health);
        healthBarVision.GetComponent<HealthBar>().setMaxHealth(health);
        healthBar.GetComponent<HealthBar>().setHealth(health);
        healthBarVision.GetComponent<HealthBar>().setHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamnge(int damnge)
    {
        // healthReduce();
        photonView.RPC("healthReduce", PhotonTargets.All,damnge);       
    }
    [PunRPC]
    public void healthReduce(int damnge)
    {
        health -= damnge;
        hitEffect.Play();
        if (!photonView.isMine)
        {
             healthBarVision.GetComponent<HealthBar>().setHealth(health);
             if (health < 1)
             {
                 Destroy(gameObject, 1);
             }
            //return;
        }
        else
        {
            healthBar.GetComponent<HealthBar>().setHealth(health);
            healthBarVision.GetComponent<HealthBar>().setHealth(health);
            if (health < 1)
            {
                gameOverPanel.SetActive(true);
            }
        }
       
    }
}
