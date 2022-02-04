using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject Jogador;
    private Vector3 CameraAlvo;

    public float Suavizador;

    void Start()
    {
        Jogador = GameObject.Find("Jogador");
        CameraAlvo = transform.position - Jogador.transform.position;
    }

    void Update()
    {
        Vector3 NovaPosicao = Jogador.transform.position + CameraAlvo;

        transform.position = Vector3.Slerp(transform.position,NovaPosicao,Suavizador);
    }
}
