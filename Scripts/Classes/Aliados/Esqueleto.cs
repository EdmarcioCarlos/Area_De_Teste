using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Esqueleto : MonoBehaviour
{
    [System.Serializable]
    public struct StructEsqueleto
    {
        public float    vida,
                        ataque,
                        defesa;
        public int Seguir;
        private NavMeshAgent Agente; 
        [System.Serializable]
        public struct StructAtributosDeMecanica
        {
            
            public NavMeshAgent Agente;
            public GameObject Player;
            public GameObject Alvo;
        }    
        public StructAtributosDeMecanica mecanica;
    }


    public StructEsqueleto atributos;
    void Start()
    {
        atributos.mecanica.Agente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Morte();
        Estado_Atual();
    }

    public void Estado_Atual()
    {
        if(atributos.Seguir == 0)
        {
            atributos.mecanica.Agente.destination = atributos.mecanica.Player.transform.position;
        }else
        {
            atributos.mecanica.Agente.destination = atributos.mecanica.Alvo.transform.position;
        }
    }
    public void Morte()
    {
        if(atributos.vida<=0)
        {
            atributos.mecanica.Player.GetComponent<ClasseNecromante>().atributos.mecanica.limiteInvocacao--;
            Destroy(this.gameObject);
        }
    }
}
