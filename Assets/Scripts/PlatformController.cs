using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    [SerializeField] private float force;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis(Horizontal);
        float inputVertical = Input.GetAxis(Vertical);
        
        transform.Rotate(Vector3.right * -force * inputHorizontal * Time.deltaTime);
        transform.Rotate(Vector3.forward * -force * inputVertical * Time.deltaTime);
    }
}
