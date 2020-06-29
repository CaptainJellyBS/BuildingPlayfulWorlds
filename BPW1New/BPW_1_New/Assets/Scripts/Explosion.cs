using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float minSize, maxSize;
    public float explosionTime;
    public float curSize;

    // Start is called before the first frame update

    void Start()
    {
        transform.localScale = new Vector3(minSize, minSize, minSize);
        StartCoroutine(Explode());
    }    

    IEnumerator Explode()
    {
        float t = 0;
        while(t<1.0f)
        {
            transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1) * minSize, new Vector3(1, 1, 1) * maxSize, t);
            t += (Time.deltaTime / explosionTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
