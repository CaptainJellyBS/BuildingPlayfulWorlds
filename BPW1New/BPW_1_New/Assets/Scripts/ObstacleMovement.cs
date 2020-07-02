using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    protected float obstacleSpeed;
    public float zBound;
    float scaled;
    protected Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Init(float speed, float scale, Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();

        obstacleSpeed = speed;
        transform.localScale = new Vector3(scale, scale, scale);
        rb.mass = scale;
        rb.velocity = direction * obstacleSpeed;
        scaled = scale;
    }

    // Update is called once per frame
    void Update()
    {

        //Make sure obstacles don't stop due to collisions
        if(rb.velocity.z > -obstacleSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -obstacleSpeed);
        }

        //Despawn obstacles once they're behind the player
        if(transform.position.z <= -90)
        {
            GameManager.Instance.score+=(int)Mathf.Ceil(scaled);
            Destroy(gameObject);
        }
    }
}
