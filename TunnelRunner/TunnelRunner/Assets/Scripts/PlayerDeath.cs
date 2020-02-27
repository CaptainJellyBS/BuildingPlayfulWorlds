using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    [Range(0,20)]
    float DeathDistance;
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
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if(new Vector3(transform.position.x, transform.position.y, 0).magnitude >= DeathDistance)
        {
            Die();
        }
    }

    void Die()
    {
        //Debug.Log("Death");
        //Debug.LogError("Dying has not been implemented yet");
    }
}
