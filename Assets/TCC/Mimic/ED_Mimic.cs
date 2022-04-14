using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ED_Mimic : MonoBehaviour
{
    private float Vida_Atual;
    private float Vida_AntDano;

    [Tooltip("Jogador que e anexado quando o jogador chega no ranged do COLLIDER")]
    public GameObject Alvo;
    [Tooltip("Barra de vida da UI ! deve ser ativada somente com jogador perto!")]
    public GameObject Barra_Vida;
    [Tooltip("Vetor que o mimic deve andar na direção (SEMPRE VAI ESTAR DENTRO DA HIERARQUIA)")]
    public Transform Local_Final;
    [Tooltip("Tempo que deve aguar para o proximo Dash")]
    public float    Proximo_Dash;
    [Tooltip("Velocidade da ação do dash")]
    public float    Velocidade_Dash;
    [Tooltip("Varive que permite ele calcular a rota do proximo dash")]
    public bool Liberado;
    [Tooltip("Controla as animaçoes do MIMIC")]
    public Animator Controlador_Animator;

    private INIStatus RefVida;
    void Start()
    {
        Vida_AntDano = Vida_Atual;
        Controlador_Animator = this.gameObject.GetComponent<Animator>();
        Barra_Vida.SetActive(false);
        Liberado = true;
    }
    void Update()
    {
        Vida_Atual = RefVida.vida;
        Controlador_Animator.SetFloat("Vida", Vida_Atual);

        if(Vida_Atual<=0)
        {
            Destroy(this.gameObject,1f);
        }else
        {
            Seguir();
        }
    }
    private void OnTriggerEnter( Collider Outro)
    {
        if(Outro.gameObject.tag =="Player")
        {
            Controlador_Animator.SetBool("Alvo", true);
            Barra_Vida.SetActive(true);
            Alvo = Outro.gameObject;
        }
    }
    public void Verificar_Dano()
    {
        if(Vida_AntDano!=Vida_Atual)
        {
            Vida_AntDano=Vida_Atual;
            Controlador_Animator.SetBool("Receber Dano", true);
            Controlador_Animator.SetBool("Receber Dano", false);
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
        Controlador_Animator.SetBool("Atacar",true);
        this.gameObject.transform.DOMove(new Vector3 (Local_Final.position.x,transform.position.y,Local_Final.position.z),Velocidade_Dash).SetEase(Ease.InCirc).OnComplete(() =>Liberado = true);
    }
}