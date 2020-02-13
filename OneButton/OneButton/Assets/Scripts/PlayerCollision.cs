using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        string colTag = col.collider.tag;
        Debug.Log("HEWWO, I AM A " + colTag);
        
    }
}
