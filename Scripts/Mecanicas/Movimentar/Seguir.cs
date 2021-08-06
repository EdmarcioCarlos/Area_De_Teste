using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Animations;

public class Seguir : MonoBehaviour
{
    #region(TESTE)
    public float Vida;
    #endregion


    private GameObject Alvo;
    private Vector3 Direcao;
    public float Velocidade;
    public float Distancia;
    private Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Alvo = GameObject.Find("Player");
    }
    void Update()
    {
        #region(Seguir Player e ativar animação)
        if (Alvo != null)
        {
            Direcao = Alvo.transform.position - transform.position;
            float AreaPalyer = Direcao.magnitude;
            Direcao = Direcao.normalized;

            if (AreaPalyer > Distancia)
            {
                Animator.SetBool("Andar", true);
                transform.position += Direcao * Time.deltaTime * Velocidade;
                transform.forward = Direcao;
            }
            else
            {
                Animator.SetBool("Andar", false);
            }
        }
        #endregion
        #region(Ativa Animações de Ataque e de Morte)
        if (Input.GetKey(KeyCode.Q))
        {
            Animator.SetBool("Ataque Normal", true);
        }
        else
        {
            Animator.SetBool("Ataque Normal", false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Animator.SetBool("Ataque Especial", true);
        }
        else
        {
            Animator.SetBool("Ataque Especial", false);
        }
        if (Vida<=0)
        {
            Animator.SetBool("Vida", false);
        }
        else
        {
            Animator.SetBool("Vida", true);
        }
        #endregion
    }
}