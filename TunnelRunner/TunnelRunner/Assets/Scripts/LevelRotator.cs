using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotator : MonoBehaviour
{
    PlayerPhysics phys;
    Rigidbody rb;
    private void Awake()
    {
        phys = FindObjectOfType<PlayerPhysics>();
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
        Quaternion prevRot = transform.rotation;
        Quaternion rot = Quaternion.AngleAxis(-phys.gravObj.myAngle, Vector3.forward);
        if (prevRot != rot)
        { 
            transform.rotation = rot;
            //phys.rb.velocity = rot* phys.rb.velocity;
        }
    }
}
