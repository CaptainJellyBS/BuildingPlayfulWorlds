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
            move.canMove = true;
            phys.grav = true;
            PlatformRotator oldGO = phys.gravObj;
            phys.gravObj = collision.collider.gameObject.GetComponent<PlatformRotator>();
            move.onGround = true;

            if(phys.gravObj == oldGO) { return; }

            Vector3 convertedHitPoint = Quaternion.AngleAxis(-phys.gravObj.myAngle + oldGO.myAngle, Vector3.forward) * transform.position;
            //convertedHitPoint.y += 0.6f;
            transform.position = convertedHitPoint;
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
