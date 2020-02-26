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
        Quaternion rotTest = Quaternion.LookRotation(transform.forward, new Vector3(-transform.position.x, -transform.position.y, 0));
        transform.rotation = rotTest;
    }
}
