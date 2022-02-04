using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ED_Tiro : MonoBehaviour
{
    [Tooltip("Dano que deve ser aplicado em todos na area")]
    public float    Dano;
    [Tooltip("Distancia maxima que a bola deve pecorer ( LEMBRE DITANCIA - ORIGEM)")]
    public float    Distancia;
    [Tooltip("Velocidade que o tiro deve pecorrer")]
    public float    Velocidade;
    [Tooltip("Objeto que esta dentro da Hierarquia do Tiro")]
    public Transform Destino;
    [Tooltip("Local onde o tiro e intancia no primeiro segundo")]
    public Vector3 Origem;
    public List<GameObject> Area;
    void Start()
    {
        Origem = this.gameObject.transform.position;
    }

    void Update()
    {
        this.gameObject.transform.DOMove(Destino.transform.position,Velocidade);
        if(Distancia <= Vector3.Distance(Origem,Destino.transform.position))
        {
            Aplicar_Dano();
        }
    }
    public void Aplicar_Dano()
    {
        for (int Contador = 0; Contador < Area.Count; Contador++)
        {
            /*
            Lembrar de troca o codigo de vida pra o de vida do Ian
            */
            Area[Contador].gameObject.GetComponent<Inimigo>().Vida -= Dano;
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision Bateu)
    {
        if(Bateu.gameObject.tag == "Inimigo")
        {
            Aplicar_Dano();
        }
    }
#region (Adicionar e Remover da lista na Area)
    private void OnTriggerEnter(Collider Entrou)
    {
        if(Entrou.gameObject.tag == "Inimigo")
        {
            Area.Add(Entrou.gameObject);
        }
    }
    private void OnTriggerExit(Collider Saiu)
    {
        if(Saiu.gameObject.tag == "Inimigo")
        {
            Area.Remove(Saiu.gameObject);
        }
    }
#endregion
}