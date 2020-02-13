using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
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
        rb.AddForce(0, 0, 1000 * Time.deltaTime);
    }

    void HandleInput()
    {

    }
}
