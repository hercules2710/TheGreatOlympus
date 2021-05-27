using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
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
    public void TakeDamnge(float Damnge)
    {
        health -= Damnge;
        healthBar.GetComponent<HealthBar>().setHealth(health);
        if (health <= 0)
        {
            destroyingEffect.Play();
            SpiderBoss.Instance.shielding = false;
            SpiderBoss.Instance.TimeUpdate();
            Destroy(gameObject, DestroyTime);
        }
    }
}
