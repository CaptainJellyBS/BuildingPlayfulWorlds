using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerPhysics phys;
    PlayerMovement move;
    Rigidbody rb;
    int uglyFix = 0;

    private void Awake()
    {
        phys = GetComponentInParent<PlayerPhysics>();
        move = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody>();
    }
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
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;

        if(collision.collider.tag == "Floor")
        {
            phys.grav = true;
            move.onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Floor")
        {
            move.onGround = false;
        }
    }
}
