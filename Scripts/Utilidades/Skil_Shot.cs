using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skil_Shot : MonoBehaviour
{
    [Tooltip(" Tempo em segundos para o objeto ser destruido")]
    public float Tempo_Destruir;
    [Tooltip(" Velocidade que objeto vai se mover")]
    public float Velocidade;
    [Tooltip(" Dano da SKill  !!! LEMBRANDO QUE ESSE VARIAL RECEBE O VALOR EM ATRIBUTOS NA PARTE DE INSTANCIAR !!!")]
    public float Dano_Da_Skill;

    [Tooltip(" Referencia do Lançador da Habilidade")]
    public GameObject Origem;
    public GameObject Alvo;

    private Rigidbody Corpo;

    void Start()
    {
        Corpo = GetComponent<Rigidbody>();

        StartCoroutine(Destruir(Tempo_Destruir));
    }
    void Update()
    {
        Corpo.velocity = transform.forward * Velocidade;
    }
    public IEnumerator Destruir(float _Tempo_Destruir)
    {
        yield return new WaitForSeconds(_Tempo_Destruir);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider Enconstei)
    {
        if(Enconstei.gameObject.CompareTag("Inimigo"))
        {
            Alvo = GameObject.Find("Inimigo");

            // Aplicar o dano no Alvo
            Enconstei.GetComponent<Atributos>().Vida = Enconstei.GetComponent<Atributos>().Vida - (Dano_Da_Skill * Vantagens(Origem,Alvo));
            Atributos.Mudar ++;
            Destroy(this.gameObject);
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
}
