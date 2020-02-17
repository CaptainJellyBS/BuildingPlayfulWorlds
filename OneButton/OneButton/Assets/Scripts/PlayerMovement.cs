using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerPhysics phys;
    [SerializeField]
    PlayerCollision col;
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    [Range(0.1f,5.0f)]
    float moveSpeed = 1.0f;
    bool pressedSpace = false, canFlip = true;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If spacebar gets pressed, and gravity can flip, set pressedSpace to true. 
        //I had trouble detecting space only being pressed once while also keeping physics in FixedUpdate, so used a little boolean magic to fix it

        if (Input.GetKeyDown(KeyCode.Space) && canFlip)
        {
            canFlip = false; pressedSpace = true;
        }
        canFlip = (Input.GetKeyUp(KeyCode.Space) || canFlip); //Set canFlip to true if space was released or it was already true. Player must also be on ground.
    }

    void FixedUpdate()
    {
        if(pressedSpace)
        {
            pressedSpace = false;
            phys.FlipGravity();
        }

        playerTransform.Translate(0, 0, moveSpeed);
    }
}
