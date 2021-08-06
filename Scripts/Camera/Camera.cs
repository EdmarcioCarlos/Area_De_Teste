using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public GameObject Player;
    private Vector3 CameraAlvo;

    public float Suavizador;

    void Start()
    {
        CameraAlvo = transform.position - Player.transform.position;
    }

    void Update()
    {
        Vector3 NovaPosicao = Player.transform.position + CameraAlvo;

        transform.position = Vector3.Slerp(transform.position,NovaPosicao,Suavizador);
    }
}
