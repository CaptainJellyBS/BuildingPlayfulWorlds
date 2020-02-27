using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerPhysics phys;
    Rigidbody rb;
    int uglyFix = 0;

    private void Awake()
    {
        phys = GetComponentInParent<PlayerPhysics>();
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
        phys.grav = true;
        phys.gravObj = collision.gameObject.GetComponent<PlatformRotator>();
        rb.velocity = Vector3.zero;
    }
}
