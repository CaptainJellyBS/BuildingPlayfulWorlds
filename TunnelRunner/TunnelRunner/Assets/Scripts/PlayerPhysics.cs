using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RecalculateGravity();
        RecalculateRotation();
    }

    void RecalculateGravity()
    {
        Physics.gravity = new Vector3(transform.position.x, transform.position.y, 0).normalized;
    }

    void RecalculateRotation()
    {
        //float rotAngle = Vector3.Angle(Vector3.up, new Vector3(transform.position.x, transform.position.y, 0).normalized);

        //transform.RotateAround(transform.position, transform.forward, rotAngle);
        //(transform.forward, rotAngle);
        Quaternion rotTest = Quaternion.LookRotation(transform.forward, new Vector3(transform.position.x, transform.position.y, 0));
        transform.rotation = rotTest;
    }
}
