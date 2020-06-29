using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerMovement player;
    public ObstacleSpawner spawner;
    public Text introText1, introText2, flightControlText1, flightControlText2;
    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame updat
    void Start()
    {
        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        introText1.enabled = true; introText2.enabled = false; flightControlText1.enabled = false; flightControlText2.enabled = false;
        yield return player.StartCoroutine(player.IntroRoutine());

        introText1.enabled = false;
        yield return new WaitForSeconds(0.5f);

        introText2.enabled = true;
        yield return new WaitForSeconds(2.0f);

        player.canMove = true;
        introText2.enabled = false;
        yield return new WaitForSeconds(0.5f);

        flightControlText1.enabled = true;
        yield return player.StartCoroutine(player.TutorialLateral());

        flightControlText1.enabled = false;
        yield return new WaitForSeconds(0.5f);

        flightControlText2.enabled = true;
        yield return player.StartCoroutine(player.TutorialVertical());

        flightControlText2.enabled = false;
        spawner.StartSpawning();
    }
}
