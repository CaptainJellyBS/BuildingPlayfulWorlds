using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public PlatformRotator gravObj;
    public bool grav;
    public Rigidbody rb;
    public float gravMod;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    void Start()
    {
        grav = true;
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
        if (grav)
        {
            Physics.gravity = -gravObj.transform.up;
        }
        else
        {
            Physics.gravity = Vector3.zero;
        }
    }

    void RecalculateRotation()
    {
        //transform.rotation = Quaternion.LookRotation(transform.forward, gravObj.transform.up);
        //transform.Rotate(Vector3.forward, gravObj.myAngle);

        //transform.Rotate(0,0,-transform.rotation.eulerAngles.z);
    }
}
