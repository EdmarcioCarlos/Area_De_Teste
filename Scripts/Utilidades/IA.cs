using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
#region (Atributos Inspector)
    [Header("Status")]
    public Satributos Atributos;
    [System.Serializable]
    public struct Satributos
    {
    public enum Tipo_Moon { Agua,Fogo,Terra,Ar,Planta,Nebula,Lama,Gelo,Pantano,Lava,Energia,Luz,Trevas,Deserto,Madeira,Inseto}
    [Tooltip("Tipo do Moon : !!! ATENÇÃO ISSO ALTERA SUAS HABILIDADES !!! ")]
    public Tipo_Moon Tipo;


    [Tooltip("Velocidade que se move")]
    [Range(0,10)]
    public float Velocidade;
    
    [Tooltip("Area para poder Atacar Inimigos")]
    [Range(0,100)]
    public float Ranged_Ataque;

    [Tooltip("Area para poder Atacar Inimigos FISICAMENTE")]
    [Range(0,5)]
    public float Ranged_Ataque_Melle;

    [Range(0,100)]
    public float    Vida,
                    Defesa,
                    Dano;
    }
#endregion    
#region (Inspector Ações)
    
    [Header("Açôes")]
    public Enum_Acoes Acoes;
    [System.Serializable]
    public struct Enum_Acoes
    {
        [Tooltip("Tempo em segundos que leva para tomar proxima ação")]
        [Range(0,10)]
        public float Coldow_Acao;

        [Tooltip("Tempo de duração dos Buffs")]
        [Range(0,10)]
        public float Tempo_Buff;

        [Tooltip("Tempo para liberar proximo Ataque")]
        [Range(0,5)]
        public float Tempo_Proximo_Ataque;

        [Tooltip("Açoes que estão ativas no momento")]
        public bool Def_Usado,
                    Curar_Usado,
                    Velocida_Usado,
                    Ataque_Normal,
                    Ataque_Espescial;
    }
#endregion
#region (Skill)
[Header("Skill")]
    public Enum_Skill Skill;
    [System.Serializable]
    public struct Enum_Skill
    {
        public GameObject Skill_Shot;
    }
#endregion

    private float Tempo_Real = 0; // Tempo que esta decorento dentro do jogo
    private int Aleatorio = 0;
    private GameObject Alvo;
    private Rigidbody Corpo;
    
    void Start()
    {
        Corpo = GetComponent<Rigidbody>();
        PegarAlvo();
    }

    void Update()
    {
        Tempo_Real += Time.deltaTime;
        if(Tempo_Real >= Acoes.Coldow_Acao)
        {
            Tempo_Real=0;
            Aleatorio = Random.Range(0,4);
            switch (Aleatorio)
            {
                case 1 : Rotacionar();
                break;

                case 2 : Atacar();
                break;

                case 3 : Buff();
                break;
            }
        }else
        {
            Mover();
        }
    }

    private void PegarAlvo()
    {
        Alvo = GameObject.Find("Player");
    }

#region (Ações)
    private void Mover()
    {
        Corpo.velocity=transform.forward*Atributos.Velocidade;
        //Ativar animação de andar aqui 
    }
    private void Rotacionar()
    {
        float ValorRotacaoAleatorio = Random.Range(-360,360);
        transform.Rotate(new Vector3(0,ValorRotacaoAleatorio,0), Space.World);
    }

    private void Atacar()
    {
        //Verifica se o Alvo esta dentro da Ranged de Ataque.
        if(Alvo.transform.position.magnitude <= Atributos.Ranged_Ataque)
        {
            //Habilita os ataques do Moon baseado em seu atributo
            switch(Atributos.Tipo)
            {
                case Satributos.Tipo_Moon.Agua :
                    switch(Aleatorio = Sorteio_Ataque(Aleatorio))
                    {
                        case 1: if(Acoes.Ataque_Normal == false)StartCoroutine(Ataque_N(Acoes.Tempo_Proximo_Ataque));
                        break;
                        case 2: if(Acoes.Ataque_Espescial == false)StartCoroutine(Ataque_E(Acoes.Tempo_Proximo_Ataque));
                        break;
                    }
                break;
                /*
                case Satributos.Tipo_Moon.Fogo :   Debug.Log("Jato de Fogo");
                break;
                case Satributos.Tipo_Moon.Terra :  Debug.Log("Golpe de Terra");
                break;
                case Satributos.Tipo_Moon.Ar :     Debug.Log("Jato de Ar");
                break;
                case Satributos.Tipo_Moon.Planta : Debug.Log("Ataque de Raizes");
                break;
                case Satributos.Tipo_Moon.Nebula : Debug.Log("Sopro do nevoeiro");
                break;
                case Satributos.Tipo_Moon.Lama :   Debug.Log("Poça de lama");
                break;
                case Satributos.Tipo_Moon.Gelo :   Debug.Log("Piso Congelante");
                break;
                case Satributos.Tipo_Moon.Pantano: Debug.Log("Alagamento pantonico");
                break;
                case Satributos.Tipo_Moon.Lava :   Debug.Log("Piso de lava");
                break;
                case Satributos.Tipo_Moon.Energia: Debug.Log("Feixe de eletron");
                break;
                case Satributos.Tipo_Moon.Luz :    Debug.Log("Raio de Luz");
                break;
                case Satributos.Tipo_Moon.Trevas : Debug.Log("Oclusão de oblivion");
                break;
                case Satributos.Tipo_Moon.Deserto: Debug.Log("Tesmpestade de Areia");
                break;
                case Satributos.Tipo_Moon.Madeira: Debug.Log("Floresta Insurgente");
                break;
                case Satributos.Tipo_Moon.Inseto : Debug.Log("Paralisia do Insetosauro");
                break; 
                */
            }
        }
        Mover();
    }

    private void Buff()
    {
        //Habilita os Buffs do Moon baseado em seu atributo
        switch(Aleatorio = Sorteio(Aleatorio))
        {
            case 1: if(Acoes.Curar_Usado == false)StartCoroutine(Curar());
            break;
            case 2: if(Acoes.Def_Usado == false)StartCoroutine(Aumentar_Defesa());
            break;
            case 3: if(Acoes.Velocida_Usado == false)StartCoroutine(Aumentar_Velocidade());
            break;
        }
       Mover();
    }
#endregion

#region (Coldow Ataques)
public IEnumerator Ataque_N(float _Temp_Ataque)
{
    Acoes.Ataque_Normal=true;
    switch(Atributos.Tipo)
    {
        case Satributos.Tipo_Moon.Agua:
        //transform.LookAt(Sorteio_Alvo_Ataque(Alvo));
        transform.LookAt(Alvo.transform.position);
        JogarSkill();
        //Colocar iskill aqui em cada case
        break;
        //Colocar restantes dos tipos de moon para verificar.
    }
    yield return new WaitForSeconds(_Temp_Ataque);
    Acoes.Ataque_Normal=false;
}
public IEnumerator Ataque_E(float _Temp_Ataque)
{
    Acoes.Ataque_Espescial=true;
    switch(Atributos.Tipo)
    {
        case Satributos.Tipo_Moon.Agua: 
        //transform.LookAt(Sorteio_Alvo_Ataque(Alvo));
        transform.LookAt(Alvo.transform.position);
        JogarSkill();
        //Colocar iskill aqui em cada case
        break;
        //Colocar restantes dos tipos de moon para verificar.
    }
    yield return new WaitForSeconds(_Temp_Ataque);
    Acoes.Ataque_Espescial=false;
}
#endregion

#region (Buffs)
    public IEnumerator Aumentar_Defesa()
    {
        Acoes.Def_Usado = true;
        float Guarda_Defesa = Atributos.Defesa;
        Atributos.Defesa +=10;
        yield return new WaitForSeconds(Acoes.Tempo_Buff);
        Atributos.Defesa = Guarda_Defesa;
        Acoes.Def_Usado=false;
    }
    public IEnumerator Aumentar_Velocidade()
    {
        Acoes.Velocida_Usado = true;
        float Guarda_Velocidade = Atributos.Velocidade;
        Atributos.Velocidade +=2;
        yield return new WaitForSeconds(Acoes.Tempo_Buff);
        Atributos.Velocidade = Guarda_Velocidade;
        Acoes.Velocida_Usado = false;
    }
    public IEnumerator Curar()
    {
        Acoes.Curar_Usado = true;
        Atributos.Vida+=10;
        yield return new WaitForSeconds(Acoes.Tempo_Buff);
        Acoes.Curar_Usado = false;
    }
#endregion
    
    //Somente sortei um numero aleatorio ate 3
    public int Sorteio(int _Aleatorio)
    {
        _Aleatorio = Random.Range(0,4);
        return _Aleatorio;
    }
    public int Sorteio_Ataque(int _Aleatorio)
    {
        _Aleatorio = Random.Range(0,3);
        return _Aleatorio;
    }

    public Vector3 Sorteio_Alvo_Ataque(GameObject _Alvo)
    {
        // Valor da margem para a iskil errar
        float _Margem_De_Erro = 5f;

        //Array de vetores para as 4 possições
        Vector3[] Alvos;
        Alvos= new Vector3[5];
        Alvos[0] = _Alvo.transform.forward*_Margem_De_Erro;
        Alvos[1] =_Alvo.transform.right*_Margem_De_Erro;
        Alvos[2] = _Alvo.transform.right*_Margem_De_Erro*-1;
        Alvos[3] = _Alvo.transform.forward*_Margem_De_Erro*-1;
        Alvos[4] = _Alvo.transform.position;
        int _Aleatorio = Random.Range(0,5);
        return Alvos[_Aleatorio];
    }
    //Somente desenha a Area de ataque da IA
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow ;
        Gizmos.DrawWireSphere (transform.position, Atributos.Ranged_Ataque);

        Gizmos.color = Color.magenta ;
        Gizmos.DrawWireSphere (transform.position, Atributos.Ranged_Ataque_Melle);
    }

    public void JogarSkill()
    {
        Instantiate(Skill.Skill_Shot, this.gameObject.transform.position, this.transform.rotation);
    }
}