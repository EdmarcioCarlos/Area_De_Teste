using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float Vida;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vida<=0)
        {
            Destroy(this.gameObject);
        }
    }
}