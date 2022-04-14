using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ED_Pause : MonoBehaviour
{

    public static bool Jogo_Pausado = false;
    public GameObject Menu_UI_Pausa;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Jogo_Pausado)
            {
                Retornar();
            }else
            {
                Pausado();
            }
        }   
    }

    public void Retornar()
    {
        Menu_UI_Pausa.SetActive(false);
        Time.timeScale= 1f;
        Jogo_Pausado = false;   
    }
    public void Pausado()
    {
        Menu_UI_Pausa.SetActive(true);
        Time.timeScale= 0f;
        Jogo_Pausado = true;   
    }


    /* Caso queiram voltar ao menur inicial no pause coloca codigo aqui pf
    */
    public void Retornar_MenuInicial()
    {
        Debug.Log("Voltando ao menu inicial ...");
    }

    
    /* Caso queiram sair do jogo no menur de pause coloca codigo aqui pf
    */
    public void Sair()
    {
        Debug.Log("Saindo do jogo.");
    }
}
