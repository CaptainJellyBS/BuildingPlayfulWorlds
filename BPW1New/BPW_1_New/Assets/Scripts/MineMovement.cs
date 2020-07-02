using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMovement : ObstacleMovement
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Init(float speed, float scale, Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();

        obstacleSpeed = speed;
        transform.localScale = new Vector3(scale, scale, scale);
        rb.mass = scale;

        direction = PlayerMovement.Instance.transform.position - transform.position;
        rb.velocity = direction * obstacleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (PlayerMovement.Instance.transform.position - transform.position).normalized;
        rb.velocity = direction * obstacleSpeed;

        //Despawn obstacles once they're behind the player
        if (transform.position.z <= -90)
        {
            GameManager.Instance.score++;
            Destroy(gameObject);
        }

        //DEBUG
        if(Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    public void Explode()
    {
        GameManager.Instance.score += 5;

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
