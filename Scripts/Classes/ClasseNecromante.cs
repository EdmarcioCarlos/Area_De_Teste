using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasseNecromante : MonoBehaviour
{

    public enum Classe{Necromante,Invoker}
    public enum Arma{Cajado,Grimorio}
    

    [System.Serializable]
    public struct StructAtibutos
    {
        public Classe   classeAtual;
        public Arma     armaAtual;
        public float    vida,
                        mana,
                        ataque,
                        defesa,
                        dinheiro;

        [System.Serializable]
        public struct StructAtributosDeMecanica
        {
            public Mesh NecromanteMesh;
            public Mesh InvokerMesh;
            public Material NecromanteMaterial;
            public Material InvokerMaterial;
            public GameObject Lancador;
            

            [Tooltip("Permite o jogado trocar sua classe essa varial so e habilitade perto da entidade !!! 0 não troca 1 pode trocar !!!")]
            public int pode_trocar_classe;

            [Tooltip("O valor dessa variavel altera a força da skil do jogador e determina o level da mesma")]
            public int Poder1;
            [Tooltip("O valor dessa variavel altera a força da skil do jogador e determina o level da mesma")]
            public int Poder2;
            [Tooltip("Dermina quantas invocaçoes estão ativas")]
            public int limiteInvocacao;

            public GameObject SangueSuga;
        }    
        public StructAtributosDeMecanica mecanica;
    }
#region (Atributos no Maximo para controle de regeneração)
    public float   vidaMax,
                    manaMax,
                    defesaMax,
                    ataqueMax;
#endregion
    public StructAtibutos atributos;
    public GameObject[] invocacao_Necromancer;
    public GameObject[] invocacao_Invoker;

    public void Start()
    {
        atributos.mecanica.Poder1 = 1;
        #region (Inicializar Status Maximos)
        vidaMax = 100;
        manaMax = 100;
        defesaMax = 20;
        ataqueMax = 20;
        #endregion
        #region (Inicializar Status Atuais do jogador)
        atributos.vida = vidaMax;   
        atributos.mana = manaMax;
        atributos.defesa = defesaMax;
        atributos.ataque = ataqueMax;
        #endregion
        atributos.mecanica.Lancador = GameObject.Find("Lancador");
        
    }
    public void Update()
    {   
        Invocar();
        Lancar_Sanguesuga();
        Trocar_Classe();
        RegenerarVida();
        RegenerarMana();
    }
#region (Verificação de troca de classes)
    public void Trocar_Classe()
    {
        if(atributos.mecanica.pode_trocar_classe == 1)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(atributos.classeAtual == Classe.Necromante)
                {
                    atributos.classeAtual=Classe.Invoker;
                    atributos.armaAtual = Arma.Grimorio;
                    GetComponent<Renderer>().material = atributos.mecanica.InvokerMaterial;
                    GetComponent<MeshFilter>().mesh= atributos.mecanica.InvokerMesh; 
                }else
                {
                    atributos.classeAtual=Classe.Necromante;
                    atributos.armaAtual = Arma.Cajado;
                    GetComponent<Renderer>().material = atributos.mecanica.NecromanteMaterial;
                    GetComponent<MeshFilter>().mesh= atributos.mecanica.NecromanteMesh;                 
                }
            }
        }
    }
#endregion
#region (Rgeneração de vida e Mana)
    public void RegenerarVida()
    {
        if(atributos.vida<vidaMax)
        {
            atributos.vida += 0.5f*Time.deltaTime;
        }else
        {
            atributos.vida = vidaMax;
        }
    }
    public void RegenerarMana()
    {
        if(atributos.mana<manaMax)
        {
            atributos.mana += 0.5f*Time.deltaTime;
        }else
        {
            atributos.mana = manaMax;
        }
    }
#endregion
#region (Skill da classe)
    public void Invocar()
    {
        if(Input.GetButtonUp("Poder 1"))
        {
            if(atributos.mecanica.limiteInvocacao<3)
            {
                //Cria um esquele do necromante na sua frente baseado em seus status e multiplicando pelo level da skill
                //e utiliza tbm a variavel de poder para acesar a posição dentro o Array para saber qual invocação e do level atual
                if(atributos.classeAtual==Classe.Necromante)
                {
                    GameObject Invocado;
                    Invocado = Instantiate(invocacao_Necromancer[atributos.mecanica.Poder1-1],this.transform.forward,this.transform.rotation);
                    Invocado.GetComponent<Esqueleto>().atributos.vida = atributos.vida*atributos.mecanica.Poder1;
                    Invocado.GetComponent<Esqueleto>().atributos.ataque = atributos.vida*atributos.mecanica.Poder1;
                    Invocado.GetComponent<Esqueleto>().atributos.defesa = atributos.vida*atributos.mecanica.Poder1;
                    Invocado.GetComponent<Esqueleto>().atributos.mecanica.Player= this.gameObject;
                    atributos.mecanica.limiteInvocacao++;
                }
                else
                {
                    GameObject Invocado;
                    Invocado = Instantiate(invocacao_Invoker[atributos.mecanica.Poder1-1],this.transform.forward,this.transform.rotation);
                    Invocado.GetComponent<Esqueleto>().atributos.vida = atributos.vida*atributos.mecanica.Poder1;
                    Invocado.GetComponent<Esqueleto>().atributos.ataque = atributos.vida*atributos.mecanica.Poder1;
                    Invocado.GetComponent<Esqueleto>().atributos.defesa = atributos.vida*atributos.mecanica.Poder1;  
                    Invocado.GetComponent<Esqueleto>().atributos.mecanica.Player= this.gameObject;
                    atributos.mecanica.limiteInvocacao++;     
                }                
            }
        }
    }

    public void Lancar_Sanguesuga()
    {
        if(Input.GetButtonUp("Poder 2"))
        {
            GameObject Habilidade = Instantiate(atributos.mecanica.SangueSuga,atributos.mecanica.Lancador.transform);
            Habilidade.GetComponent<SangueSugaHabilidade>().Origem = this.gameObject;
            Habilidade.GetComponent<SangueSugaHabilidade>().Poder_Da_Skil = atributos.mecanica.Poder2;
            Habilidade.GetComponent<SangueSugaHabilidade>().Dano_Da_Skill = atributos.ataque * atributos.mecanica.Poder2;
        }
    }

#endregion
    //Sera utilizado no botão de comprar Iskill para aumentar o nivel do Poder 1
    public void Subir_Nivel_Poder1()
    {
        atributos.mecanica.Poder1++;
    }
}