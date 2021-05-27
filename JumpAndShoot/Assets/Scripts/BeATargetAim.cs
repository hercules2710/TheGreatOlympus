using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeATargetAim : MonoBehaviour
{
    public GameObject host;
    Target targetScript;
    // Start is called before the first frame update
    void Start()
    {
        targetScript = host.GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("EvaBullet"))
        {
            if(targetScript!= null)
            {
                targetScript.TakeDamnge(EvaController.Instance.danmge);
                Destroy(other.gameObject);
            }
        }
    }
}
