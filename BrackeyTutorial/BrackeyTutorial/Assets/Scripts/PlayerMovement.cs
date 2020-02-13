using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    [Range(0,5000)]
    float forwardSpeed = 1000f;

    [SerializeField]
    [Range(0, 5000)]
    float sideSpeed = 500f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("HEWWO");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        HandleInput();
        rb.AddForce(0, 0, forwardSpeed * Time.deltaTime);
    }

    void HandleInput()
    {
        if (Input.GetKey("d"))
        {
            rb.AddForce(sideSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sideSpeed * Time.deltaTime, 0, 0);
        }
    }
}
