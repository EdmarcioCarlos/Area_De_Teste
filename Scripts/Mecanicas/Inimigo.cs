using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Tooltip("Vida Do Objeto")]
    [Range(0f,100f)]
    public float Vida;

    void OnCollisionEnter(Collision Entrar)
    {
        if (Entrar != null)
        {
            Vida--;
        } // Tudo que encostar no inimigo diminui sua vida;
    }
    void Update()
    {

    }
}
