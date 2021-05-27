using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingweb : MonoBehaviour
{
    public ParticleSystem effect;
    GameObject playerPosittion;
    Vector3 playerLocation;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerPosittion = GameObject.FindGameObjectWithTag("Player");
        playerLocation = playerPosittion.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, playerLocation, speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 2f);
    }
}
