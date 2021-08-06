using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasseMetamorfo : MonoBehaviour
{
    public enum Classe{Metamorfo,Lunari}
    public enum Arma{Manopla,Khopesh}

    [System.Serializable]
    public struct StructAtibutos
    {
        public Classe classe_atual;
        public Arma arma_atual;
        public float    vida,
                        furia,
                        ataque,
                        defesa,
                        dinheiro;
        [System.Serializable]
        public struct StructAtributosDeMecanica
        {
            [Tooltip("Permite o jogado trocar sua classe essa varial so e habilitade perto da entidade !!! 0 não troca 1 pode trocar !!!")]
            public int pode_trocar_classe;

            [Tooltip("O valor dessa variavel altera a força da skil do jogador e determina o level da mesma")]
            public int Poder1; 
        }    
        public StructAtributosDeMecanica atributos_de_mecanica;
    }
#region (Atributos no Maximo para controle de regeneração)
    private float   vidaMax,
                    furiaMax,
                    defesaMax,
                    ataqueMax;
#endregion
    public StructAtibutos atributos;
        public void Start()
    {
        atributos.atributos_de_mecanica.Poder1 = 1;
        #region (Inicializar Status Maximos)
        vidaMax = 100;
        furiaMax = 100;
        defesaMax = 20;
        ataqueMax = 20;
        #endregion
        #region (Inicializar Status Atuais do jogador)
        atributos.vida = vidaMax;
        atributos.furia = furiaMax;
        atributos.defesa = defesaMax;
        atributos.ataque = ataqueMax;
        #endregion
    }
    public void Update()
    {   
        Trocar_Classe();
    }
#region (Verificação de troca de classes)
    public void Trocar_Arma()
    {
        if(atributos.classe_atual==Classe.Metamorfo)
        {
            atributos.arma_atual = Arma.Manopla;
        }else
        {
            atributos.arma_atual = Arma.Khopesh;
        }
    }
    public void Trocar_Classe()
    {
        if(atributos.atributos_de_mecanica.pode_trocar_classe==1)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if(atributos.classe_atual==Classe.Metamorfo)
                {
                    atributos.classe_atual=Classe.Lunari;
                    Trocar_Arma();
                }else
                {
                    atributos.classe_atual=Classe.Metamorfo;
                    Trocar_Arma();
                }
            }
        }
    }
#endregion
#region (Rgeneração de vida e furia)
    public void RegenerarVida()
    {
        if(atributos.vida<vidaMax)
        {
            atributos.vida++;
        }else
        {
            atributos.vida = vidaMax;
        }
    }
#endregion
#region (Skill da classe)
#endregion

    //Sera utilizado no botão de comprar Iskill para aumentar o nivel do Poder 1
    public void Subir_Nivel_Poder1()
    {
        atributos.atributos_de_mecanica.Poder1++;
    }
}