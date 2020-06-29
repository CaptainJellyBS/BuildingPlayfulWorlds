using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject baseObstacle;
    public float minimumTime, maximumTime;
    public float minimumSize, maximumSize;
    public float minimumSpeed, maximumSpeed;
    public float maxOffsetX, maxOffsetY;
    public float maxDirectionVariation;

    bool currentlySpawning = false;

    public void StartSpawning()
    {
        currentlySpawning = true;
        StartCoroutine(SpawnObstacles());
    }

    public void StopSpawning()
    {
        currentlySpawning = false;
    }

    public IEnumerator SpawnObstacles()
    {
        while(currentlySpawning)
        {
            yield return new WaitForSeconds(Random.Range(minimumTime, maximumTime));
            GameObject newObs = Instantiate(baseObstacle, new Vector3(transform.position.x + Random.Range(-maxOffsetX, maxOffsetX), transform.position.y + Random.Range(-maxOffsetY, maxOffsetY), transform.position.z), Quaternion.identity);
            newObs.GetComponent<ObstacleMovement>().Init(Random.Range(minimumSpeed, maximumSpeed), Random.Range(minimumSize, maximumSize), -Vector3.forward + new Vector3(Random.Range(-maxDirectionVariation, maxDirectionVariation), Random.Range(-maxDirectionVariation, maxDirectionVariation), 0));
        }
    }
}
