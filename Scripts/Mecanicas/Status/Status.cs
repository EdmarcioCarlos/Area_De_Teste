using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public enum EnumTipoObjeto { Player,NPC,Inimigo}
    public EnumTipoObjeto Tipo;

    #region (Informções do objeto Player)
    [System.Serializable]
    public struct _Struct_Info_Player
    {
        [Tooltip("Nome do Obejeto")]
        public string NomeObjeto;

        [Tooltip("Valor Da vida do Objeto")]
        [Range(1,100)]
        public float Vida;

        [Tooltip("Valor Da Energia do Objeto")]
        [Range(1,100)]
        public float Energia;

        [Tooltip("Valor Da Experiencia do Objeto")]
        [Range(1,100)]
        public float Experiencia;

        [Tooltip("Valor Da vida do Objeto")]
        public bool Morto;
    }
    public _Struct_Info_Player PlayerInfo;
    #endregion
    #region (Informções do objeto NPC)
    [System.Serializable]
    public struct _Struct_Info_NPC
    {
        [Tooltip("Nome do Obejeto")]
        public string NomeObjeto;

        [Tooltip("Valor Da vida do Objeto")]
        [Range(1,100)]
        public float Vida;

        [Tooltip("Informa se o NPC pode interegarir")]
        public bool Interagir;
    }
    public _Struct_Info_NPC NPC;
    #endregion
    #region (Informções do objeto Inimigo)
    [System.Serializable]
    public struct _Struct_Info_Inimigo
    {
        [Tooltip("Nome do Obejeto")]
        public string NomeObjeto;

        [Tooltip("Valor Da vida do Objeto")]
        [Range(1,100)]
        public float Vida;

        [Tooltip("Valor Da Energia do Objeto")]
        [Range(1,100)]
        public float Energia;

        [Tooltip("Valor Da Experiencia do Objeto")]
        [Range(1,100)]
        public float Experiencia;

        [Tooltip("Valor Da vida do Objeto")]
        public bool Morto;
    }
    public _Struct_Info_Inimigo Inimigo;
    #endregion
    public float ReceberDano(float _Dano)
    {
        
        PlayerInfo.Vida = PlayerInfo.Vida -_Dano;
        return PlayerInfo.Vida;
    }
    #region (Energia Adicionar e Retirar)
    public float GastarEnergia(float _EnergiaGasta)
    {
        PlayerInfo.Energia = PlayerInfo.Energia -_EnergiaGasta;
        return PlayerInfo.Energia;
    }
    public float ReceberEnergia(float _EnergiaRecebida)
    {
        PlayerInfo.Energia = PlayerInfo.Energia -_EnergiaRecebida;
        return PlayerInfo.Energia;
    }
    #endregion
    #region (Experiencia Adicionar e Retirar)
    public float ReceberExperiencia(float _ExperienciaRecebida)
    {
        PlayerInfo.Experiencia = PlayerInfo.Experiencia+_ExperienciaRecebida;
        return PlayerInfo.Experiencia;
    }
        public float PerdeExperiencia(float _ExperienciaPerdida)
    {
        PlayerInfo.Experiencia = PlayerInfo.Experiencia+_ExperienciaPerdida;
        return PlayerInfo.Experiencia;
    }
    #endregion
    public void Morte()
    {
        Destroy(this.gameObject,1f);
    }
}