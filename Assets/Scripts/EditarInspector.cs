using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditarInspector : MonoBehaviour
{
    
    public enum EnumTipoATK { Meele,Range}
    public enum EnumClasse { Dps,Tank,Healer}

    public enum EnumTipoPersonagem { Inimigo, Jogador, Quest, Dialogo }


    [System.Serializable]
    public struct EnumPersonagem
    {
        public EnumTipoPersonagem TipoPersonagem;

        [Tooltip("Nome do Jogador")]
        public string Nome;

        [Tooltip("Status do Jogador")]
        [Range(1,100)]
        public int HP,
                   DEF,
                   FOR;

        public EnumTipoATK ATK;
        public EnumClasse Tipo;

    }
    [Header("Dados do personagem")]
    public EnumPersonagem Personagem;

    [HideInInspector]
    public string senha;

    [ContextMenu("OLA MEU XUXU LINDAO")]
    public void Oii()
    {
        Debug.Log("OLAAA");
    }

    public void OnDrawGizmos ()
    {
        
        Gizmos.DrawIcon(this.transform.position, "Inimigo.png");

        if (Personagem.TipoPersonagem == EnumTipoPersonagem.Jogador)
        {
            Gizmos.DrawIcon(this.transform.position, "Jogador.png");
        }

    }


    /*
     * [Tooltip("")] ==> e ultilizado pra descrever uma variel no ispector.
     * [Header("")] ==> e utilizado para criar um novo cabeçalho no inspector.
     * [Range()] ==> Limita o valor de uma variavel pode chegar no inspector.
     * [Multiline(Numero de linhas)] ==> Gera uma aba de texto no inspector que pode ser edita por la.
     * 
     * [ContextMenu("Digite o que quiser")] ==> clicando com o botão direito no codigo no inspector vc pode executar um botao.
     * Gizmos.DrawIcon(this.transform.position, "NomeDoAquivo.PNG"); ==> Gera o icone com o nome do arquivo na coordenado do Objeto.
     * 
     * [System.Serializable] ==> deve ser ultilizado antes do struct pra mostra o struct no inspector exemplo :
     * O serializable para com o inicio e final da primeira {}.
     * [System.Serializable]
       public struct Sjogador
       {
           [Tooltip("Nome do Jogador")]
           public string Nome;
           [Tooltip("Status do Jogador")]
           public int HP,
                      DEF,
                      FOR;
       }


     */
}
