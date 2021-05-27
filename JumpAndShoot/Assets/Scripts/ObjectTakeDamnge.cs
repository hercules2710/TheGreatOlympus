using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTakeDamnge : MonoBehaviour
{
    float health = 100f;
    GameObject Gun;
    public GameObject healthBar;
    public GameObject CityController;
    // Start is called before the first frame update
    void Start()
    {
        Gun = GameObject.FindGameObjectWithTag("Gun");
        healthBar.GetComponent<HealthBar>().setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamnge(float damnge)
    {
        health -= damnge;
        healthBar.GetComponent<HealthBar>().setHealth(health);
        if (health <= 0)
        {
            CityController.GetComponent<OpenGate>().isOpenGate = true;
            CityController.GetComponent<TheCityControll>().OpenGate();
            Destroy(gameObject, 0.7f);
        }
    }
}
