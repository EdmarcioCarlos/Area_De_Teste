using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacionar : MonoBehaviour {
    public bool local = false;
    public Vector3 VEL=Vector3.zero;
	void Update () {
        if (local)
        {
            transform.Rotate(VEL * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Rotate(VEL * Time.deltaTime, Space.World);
        }
	}
}