using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ED_Mimic : MonoBehaviour
{
    [Tooltip("Jogador que e anexado quando o jogador chega no ranged do COLLIDER")]
    public GameObject Alvo;
    [Tooltip("Vetor que o mimic deve andar na direção (SEMPRE VAI ESTAR DENTRO DA HIERARQUIA)")]
    public Transform Local_Final;
    [Tooltip("Tempo que deve aguar para o proximo Dash")]
    public float    Proximo_Dash;
    [Tooltip("Velocidade da ação do dash")]
    public float    Velocidade_Dash;
    [Tooltip("Varive que permite ele calcular a rota do proximo dash")]
    public bool Liberado;
    void Start()
    {
        Liberado = true;
    }
    void Update()
    {
        Seguir();
    }
    private void OnTriggerEnter( Collider Outro)
    {
        if(Outro.gameObject.tag =="Player")
        {
            Alvo = Outro.gameObject;
        }
    }
    void Seguir()
    {
        if(Alvo!=null && Liberado==true)
        {
            //Dash estilo Xadrez
            StartCoroutine("Bauzada");
        }
    }
    IEnumerator Bauzada()
    {
        Liberado = false;
        yield return new WaitForSeconds(Proximo_Dash);
        //movimento
        this.gameObject.transform.LookAt(new Vector3 (Alvo.transform.position.x,transform.position.y,Alvo.transform.position.z));
        this.gameObject.transform.DOMove(new Vector3 (Local_Final.position.x,transform.position.y,Local_Final.position.z),Velocidade_Dash).SetEase(Ease.InCirc).OnComplete(() =>Liberado = true);
    }
}