using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerMovement player;
    ObstacleSpawner spawner;
    ObstacleSpawner mineSpawner;
    public Text introText1, introText2, flightControlText1, flightControlText2, flightControlText3, flightControlText4, introText3;
    public Text timeValue, scoreValue;
    public Text deathScoreValue;
    public GameObject timeScorePanel;
    public bool currentlyTiming;
    public int score = 0;

    public GameObject deathPanel;
    public GameObject pausePanel;

    public AmmoPanel[] ammoPanels;
    public Slider masterSlider, sfxSlider, musicSlider, voiceSlider;


    public ObstacleSpawner[] spawners;
    public ObstacleSpawner[] mineSpawners;

    bool paused = false;
    float timer = 0;
    bool tutorialFinished = false;
    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame updat
    void Start()
    {
        spawner = spawners[(int)MiscPersistentData.Instance.currentLevel];
        mineSpawner = mineSpawners[(int)MiscPersistentData.Instance.currentLevel];

        deathPanel.SetActive(false);
        pausePanel.SetActive(false);

        introText1.enabled = false; introText2.enabled = false; flightControlText1.enabled = false;
        flightControlText2.enabled = false; flightControlText3.enabled = false; flightControlText4.enabled = false;  introText3.enabled = false;
        timeScorePanel.SetActive(false);

        foreach (AmmoPanel a in ammoPanels) { a.gameObject.SetActive(false); }
  
        if (paused) { TogglePause(); } //In case we paused and then went back to main menu, unpause

        if (MiscPersistentData.Instance.currentLevel == DifficultyLevel.Cadet) { StartCoroutine(Tutorial()); }
        else { StartCoroutine(NormalStart()); }
        
    }

    void Update()
    {
        UpdateTimer();
        scoreValue.text = score.ToString("000000");

        if(Input.GetKeyDown(KeyCode.Escape))
        { TogglePause(); }
    }
    
    /// <summary>
    /// Tutorial sequence that teaches the controles
    /// </summary>
    /// <returns></returns>
    IEnumerator Tutorial()
    {
        introText1.enabled = true;
        AudioM.Instance.PlayVoiceLine(0);
        yield return player.StartCoroutine(player.IntroRoutine());

        while(AudioM.Instance.IsLinePlaying(0))
        {
            yield return null;
        }

        introText1.enabled = false;
        yield return new WaitForSeconds(0.5f);

        introText2.enabled = true;
        AudioM.Instance.PlayVoiceLine(1);
        
        while (AudioM.Instance.IsLinePlaying(1))
        {
            yield return null;
        }
        player.canMove = true;

        introText2.enabled = false;
        yield return new WaitForSeconds(0.5f);

        flightControlText1.enabled = true;
        AudioM.Instance.PlayVoiceLine(2);

        yield return player.StartCoroutine(player.TutorialLateral());
       
        while (AudioM.Instance.IsLinePlaying(2))
        {
            yield return null;
        }

        flightControlText1.enabled = false;
        yield return new WaitForSeconds(0.5f);

        flightControlText2.enabled = true;
        AudioM.Instance.PlayVoiceLine(3);

        yield return player.StartCoroutine(player.TutorialVertical());

        while (AudioM.Instance.IsLinePlaying(3))
        {
            yield return null;
        }

        flightControlText2.enabled = false;
        yield return new WaitForSeconds(0.5f);

        flightControlText3.enabled = true;
        AudioM.Instance.PlayVoiceLine(4);

        player.canShoot = true;
        foreach (AmmoPanel a in ammoPanels) { a.gameObject.SetActive(true); }
        yield return player.StartCoroutine(player.TutorialShoot());
        
        while (AudioM.Instance.IsLinePlaying(4))
        {
            yield return null;
        }

        flightControlText3.enabled = false;
        yield return new WaitForSeconds(0.5f);

        flightControlText4.enabled = true;
        AudioM.Instance.PlayVoiceLine(5);

        yield return player.StartCoroutine(player.TutorialDodge());

        while (AudioM.Instance.IsLinePlaying(5))
        {
            yield return null;
        }

        flightControlText4.enabled = false;
        introText3.enabled = true;
        AudioM.Instance.PlayVoiceLine(6);
        yield return new WaitForSeconds(3.0f);

        timeScorePanel.SetActive(true);
        spawner.StartSpawning();
        mineSpawner.StartSpawning(); 
        StartTimer(-2.0f);
        tutorialFinished = true;
        MiscPersistentData.Instance.levelsAvailable[1] = true; //Unlock Lieutenant level when finishing the tutorial

        while (AudioM.Instance.IsLinePlaying(6))
        {
            yield return null;
        }

        introText3.enabled = false;

    }

    /// <summary>
    /// Normal start without the tutorial
    /// </summary>
    /// <returns></returns>
    IEnumerator NormalStart()
    {
        tutorialFinished = true;
        foreach (AmmoPanel a in ammoPanels) { a.gameObject.SetActive(true); }

        yield return player.StartCoroutine(player.IntroRoutine());
        player.canShoot = true;
        player.canMove = true;
        timeScorePanel.SetActive(true);
        spawner.StartSpawning();
        mineSpawner.StartSpawning(); 
        timeScorePanel.SetActive(true);
        StartTimer(-2.0f);

    }

    public void Die()
    {
        HighscoreManager.Instance.AddScore(MiscPersistentData.Instance.currentLevel, MiscPersistentData.Instance.playerName, score);
        HighscoreManager.Instance.SaveHighscores();
        Time.timeScale = 0.0f;
        deathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        deathScoreValue.text = score.ToString("000000");
    }


    #region pause stuff
    public void RestartLevel()
    {
        //If we're on Cadet level, and we finished the tutorial, go to Lieutenant level to skip the tutorial
        if(MiscPersistentData.Instance.currentLevel == DifficultyLevel.Cadet && tutorialFinished)
        {
            MiscPersistentData.Instance.currentLevel = DifficultyLevel.Lieutenant;
        }
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }


    public void TogglePause()
    {
        if (!paused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            paused = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1.0f;
            paused = false;
        }
        pausePanel.SetActive(paused);
    }
    #endregion
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

    public void UpdateMasterVolume(float newVolume)
    {
        AudioM.Instance.UpdateMasterVolume(newVolume);
    }
    public void UpdateSFXVolume(float newVolume)
    {
        AudioM.Instance.UpdateSFXVolume(newVolume);
    }
    public void UpdateVoiceVolume(float newVolume)
    {
        AudioM.Instance.UpdateVoiceVolume(newVolume);
    }

    public void UpdateMusicVolume(float newVolume)
    {
        AudioM.Instance.UpdateMusicVolume(newVolume);
    }

    public void SetVolumeSliders()
    {
        masterSlider.value = AudioM.Instance.MasterVolume;
        sfxSlider.value = AudioM.Instance.SFXVolume;
        musicSlider.value = AudioM.Instance.MusicVolume;
        voiceSlider.value = AudioM.Instance.VoiceVolume;
    }
}
