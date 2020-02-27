using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotator : MonoBehaviour
{
    PlayerPhysics phys;
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
        //Quaternion rot = Quaternion.AngleAxis(-phys.gravObj.myAngle, Vector3.forward);
        //transform.rotation = rot;
    }
}
