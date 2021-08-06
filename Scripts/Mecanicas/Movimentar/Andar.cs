using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Andar : MonoBehaviour
{

    Vector3 Deslocamento = new Vector3();
    [Range(0,6)]
    public float Velocidade;
    private Rigidbody Rb;
    private Vector3 Mover;

    public void Start()
    {
        Mover = Vector3.zero;
        Rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        Movimentar_Via_Joystick();
    }
    public void Movimentar_Via_Joystick()
    {

        Mover.x = Input.GetAxis("Horizontal");
        Mover.z = Input.GetAxis("Vertical");

        Deslocamento = Rb.position;
        Rb.MovePosition(Deslocamento + Mover * Velocidade*Time.fixedDeltaTime);
        if (Mover != Vector3.zero)
        {
            transform.forward = Mover;
        }
        var velocidadeMovimento = Velocidade;
        Rb.MovePosition(Rb.position + velocidadeMovimento * Time.fixedDeltaTime * Mover);
    }
}