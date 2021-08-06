using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Jogador_Mana : MonoBehaviour
{

    private GameObject Player;
    private Slider Vida_UI;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Jogador");
        Vida_UI = this.gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        Atualizar_Mana();
    }

    public void Atualizar_Mana()
    {
        //Classe Necromante
        Vida_UI.maxValue = Player.GetComponent<ClasseNecromante>().manaMax;
        Vida_UI.value = Player.GetComponent<ClasseNecromante>().atributos.mana;

        //Classe Metamorfo
        //Vida_UI.value = Player.GetComponent<ClasseMetamorfo>().atributos.mana;
    }
}
