using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        target = hit.point;

        transform.LookAt(target);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Bullet")) { return; } //Player cannot shoot self, bullets will not destroy bullets.
        if(col.gameObject.CompareTag("Mine")) { col.gameObject.GetComponent<MineMovement>().Explode(); }
        Destroy(gameObject);
    }
}
