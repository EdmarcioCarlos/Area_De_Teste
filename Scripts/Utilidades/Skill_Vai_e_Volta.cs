using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Vai_e_Volta : MonoBehaviour
{
    [Tooltip(" Velocidade que objeto vai se mover")]
    public float Velocidade;
    [Tooltip(" Multiplicador  para calcular a Ranged Final")]
    [Range(20,50)]
    public float Ranged_Final;

    public float Dano_Da_Skill;
    public GameObject Origem;
    public GameObject Alvo;

    public Vector3 Final;



    private Rigidbody Corpo;



    void Start()
    {
        Corpo = GetComponent<Rigidbody>();
        Final =  Origem.transform.position + (this.transform.forward*Ranged_Final);
    }
    void Update()
    {
        Corpo.velocity = transform.forward * Velocidade;
        Retornar();
    }
        void OnTriggerEnter(Collider Enconstei)
    {
        if(Enconstei.gameObject.CompareTag("Inimigo"))
        {
            Alvo = GameObject.Find("Inimigo");

            // Aplicar o dano no Alvo
            Enconstei.GetComponent<Atributos>().Vida = Enconstei.GetComponent<Atributos>().Vida - (Dano_Da_Skill * Vantagens(Origem,Alvo));
        }

        if(Enconstei.gameObject.CompareTag("Jogador"))
        {
            Destroy(this.gameObject);
        }
    }

    public void Retornar()
    {
        if(this.transform.position == Final)
        {
            Debug.Log("Entrei vo retornar");    
            transform.LookAt(Origem.transform.position);
        }
    }

    #region (Vantagens)
    public float Vantagens(GameObject _Atirador, GameObject _Alvo)
    {
        float Multiplicador = 0;
        if(_Atirador.GetComponent<Atributos>().Tipo == _Alvo.GetComponent<Atributos>().Tipo)
        {
            Multiplicador = Igual();
        }
        else if (_Atirador.GetComponent<Atributos>().Tipo != _Alvo.GetComponent<Atributos>().Tipo)
        {
            // Compara o tipo do NPC para aplicar o dano correto linha 237
            switch(_Alvo.GetComponent<Atributos>().Tipo) 
            {
                case Atributos.Tipo_Objeto.Agua :
                    if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Fogo)
                    {
                        Multiplicador = Contra();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Terra)
                    {
                        Multiplicador = Inferior();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Ar)
                    {
                        Multiplicador = Igual();
                    }
                break;
                case Atributos.Tipo_Objeto.Fogo :
                    if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Agua)
                    {
                        Multiplicador = Contra();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Terra)
                    {
                        Multiplicador = Igual();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Ar)
                    {
                        Multiplicador = Inferior();
                    }
                break;
                case Atributos.Tipo_Objeto.Terra :
                    if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Agua)
                    {
                        Multiplicador = Contra();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Fogo)
                    {
                        Multiplicador = Igual();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Ar)
                    {
                        Multiplicador = Inferior();
                    }
                break;
                case Atributos.Tipo_Objeto.Ar :
                    if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Agua)
                    {
                        Multiplicador = Igual();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Fogo)
                    {
                        Multiplicador = Contra();
                    }
                    else if(_Atirador.GetComponent<Atributos>().Tipo == Atributos.Tipo_Objeto.Terra)
                    {
                        Multiplicador = Inferior();
                    }
                break;
            }
        }
        return Multiplicador;
    }
    #region (Multiplicadores)
    public float Contra()
    {
        return 2f;
    }
    public float Igual()
    {
        return 1f;
    }
    public float Inferior()
    {
        return 0.5f;
    }
    #endregion
#endregion
   void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow ;
        Gizmos.DrawWireSphere (Final, 1f);
    }
}
