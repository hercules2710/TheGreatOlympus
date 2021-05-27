using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTaget : MonoBehaviour
{
    public GameObject healthBar;
    public float health;
    public float DestroyTime;
    public ParticleSystem destroyingEffect;
  //  bool effectFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<HealthBar>().setMaxHealth(health);
        healthBar.GetComponent<HealthBar>().setHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamnge( float Damnge)
    {
        health -= Damnge;
        healthBar.GetComponent<HealthBar>().setHealth(health);
        if(health<=0)
        {
            destroyingEffect.Play();         
            Destroy(gameObject,DestroyTime);
        }
    }
}
