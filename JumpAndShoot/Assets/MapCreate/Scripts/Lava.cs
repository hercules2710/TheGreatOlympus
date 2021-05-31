using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float scrollspeed = 0.1f;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveLava = Time.time * scrollspeed;
        rend.material.mainTextureOffset =new Vector2(0, moveLava);
    }
}
