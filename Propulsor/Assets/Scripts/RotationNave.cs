using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationNave : MonoBehaviour
{
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation.z = Input.GetAxis("Horizontal") * Time.deltaTime;
    }
}
