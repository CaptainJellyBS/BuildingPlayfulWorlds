using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    public float myAngle;
    private void Awake()
    {
    }
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(transform.forward, new Vector3(-transform.position.x, -transform.position.y, 0));
        myAngle = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }
}
