using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 20.0f)]
    float baseGravity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -baseGravity, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipGravity()
    {
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y * -1.0f, Physics.gravity.z);
        Debug.Log("Switching Gravity");
        //throw new System.NotImplementedException("Gravity Flipping not yet implemented");
    }
}
