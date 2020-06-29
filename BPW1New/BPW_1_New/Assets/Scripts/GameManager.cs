using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerMovement player;
    public ObstacleSpawner spawner;
    public ObstacleSpawner mineSpawner;
    public Text introText1, introText2, flightControlText1, flightControlText2;
    public Text timeValue, scoreValue;
    public GameObject timeScorePanel;
    public bool currentlyTiming;
    public int score = 0;

    float timer = 0;
    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame updat
    void Start()
    {
        StartCoroutine(Tutorial());
        //DEBUG

        StartTimer(-2.0f);

    }

    void Update()
    {
        UpdateTimer();
        scoreValue.text = score.ToString("000000");
    }

    IEnumerator Tutorial()
    {
        introText1.enabled = true; introText2.enabled = false; flightControlText1.enabled = false; flightControlText2.enabled = false;
        timeScorePanel.SetActive(false);
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
        timeScorePanel.SetActive(true);
        spawner.StartSpawning();
        mineSpawner.StartSpawning();

    }


    #region time stuff

    void StartTimer(float startTime = 0.0f)
    {
        timer = startTime;
        currentlyTiming = true;
    }

    void StopTimer()
    {
        currentlyTiming = false;
    }

    void UpdateTimer()
    {
        if (!currentlyTiming) { return; }
        timer += Time.deltaTime;
        timeValue.text = ParseTime(timer);

    }

    /// <summary>
    /// Takes a time in seconds and returns a string formatted xx:xx:xx
    /// </summary>
    /// <param name="t">The time in seconds</param>
    /// <returns></returns>
    string ParseTime(float t)
    {
        string sign = " ";
        if (t < 0.00f)
        {
            sign = "-";
            t = Mathf.Abs(t);
        }
        int minutes = (int)(Mathf.Floor(t) / 60);
        int seconds = (int)Mathf.Floor(t) % 60;
        int milliseconds = (int)(System.Math.Round(t%1, 2)*100);

        return sign + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
#endregion
}
