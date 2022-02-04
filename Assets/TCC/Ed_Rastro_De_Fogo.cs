using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ed_Rastro_De_Fogo : MonoBehaviour
{
    [Tooltip("Dano que deve ser aplicado ( O DANO E POR SEGUNDO)")]
    public float    Dano;
    [Tooltip("Tempo para destruir o objeto")]
    public float    Tempo;

    void Start()
    {
       Destroy(this.gameObject,Tempo); 
    }

    private void OnTriggerStay (Collider Dentro)
    {
        if(Dentro.gameObject.tag == "Inimigo")
        {
            Dentro.GetComponent<Inimigo>().Vida-= Dano*Time.deltaTime;
        }
    }
}
