using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0,100)]
    float deathPlaneSpeed;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, deathPlaneSpeed * Time.deltaTime);
    }
}
