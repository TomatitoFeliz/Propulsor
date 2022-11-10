using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationNave : MonoBehaviour
{
    public Transform Personas;
    public float velocidad = 4.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Personas.Rotate(Vector3.forward * velocidad * Time.deltaTime);
    }
}
