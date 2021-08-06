using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Informacoes : MonoBehaviour
{
#region (Atributos)
    public Enum_Atributos Atributos;
    [System.Serializable]
    public struct Enum_Atributos
    {
        [Tooltip("Vida do jogador")]
        [Range(1,100)]
        public float Vida;
    }
#endregion
#region (Ui Informações)
    public Enum_Ui Ui_Informacoes;
    [System.Serializable]
    public struct Enum_Ui
    {
        [Tooltip("Barra de vida do jogador")]
        public Scrollbar Barra_De_Vida;
        [Tooltip("Bolha de vida do jogador")]
        public Image Bolha_De_Vida;
    }
#endregion
    void Start()
    {
        
    }
    void Update()
    {
        Ui_Informacoes.Barra_De_Vida.size = (Atributos.Vida/100);
        Ui_Informacoes.Bolha_De_Vida.fillAmount = (Atributos.Vida/100);
    }
}
