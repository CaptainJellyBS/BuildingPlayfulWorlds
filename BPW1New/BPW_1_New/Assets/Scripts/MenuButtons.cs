using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public int firstScene;
    public Slider masterSlider, sfxSlider, musicSlider;
    public GameObject cannotStartNoName;
    public bool canStartPlay = false;

    public Button LtButton, CptButton, ColButton, GenButton;
    public GameObject LtRequireText, CptRequireText, ColRequireText, GenRequireText;

    public Text highScoreText;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1.0f;
    }

    public void SetDifficultyButtons()
    {
        if (MiscPersistentData.Instance.levelsAvailable[1])
        { 
            LtButton.interactable = true; 
            LtRequireText.SetActive(false); 
        }
        else 
        { 
            LtButton.interactable = false; LtRequireText.SetActive(true); 
        }

        if (MiscPersistentData.Instance.levelsAvailable[2])
        {
            CptButton.interactable = true;
            CptRequireText.SetActive(false);
        }
        else
        { 
            CptButton.interactable = false; CptRequireText.SetActive(true); 
        }

        if (MiscPersistentData.Instance.levelsAvailable[3])
        {
            ColButton.interactable = true;
            ColRequireText.SetActive(false);
        }
        else
        {
            ColButton.interactable = false; ColRequireText.SetActive(true);
        }

        if (MiscPersistentData.Instance.levelsAvailable[4])
        {
            GenButton.interactable = true;
            GenRequireText.SetActive(false);
        }
        else
        { 
            GenButton.interactable = false; GenRequireText.SetActive(true);
        }
    }

    public void SetHighScoreText()
    {
        HighscoreManager.Instance.LoadHighscores();
        highScoreText.text = HighscoreManager.Instance.HighScoreString();
    }

    public void StartGame(int level)
    {
        MiscPersistentData.Instance.currentLevel = (DifficultyLevel)level;
        if (canStartPlay)
        {
            SceneManager.LoadScene(firstScene);
        }
        else
        {
            cannotStartNoName.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Set the volume sliders in the menu to the correct values
    /// </summary>
    public void SetVolumeSliders()
    {
        masterSlider.value = AudioM.Instance.MasterVolume;
        sfxSlider.value = AudioM.Instance.SFXVolume;
        musicSlider.value = AudioM.Instance.MusicVolume;
    }

    public void SetName(string name)
    {
        //Ensure name is no longer than 30 characters
        if(name.Length>=20)
        {
            name = name.Substring(0, 20);
        }

        MiscPersistentData.Instance.playerName = name;
        canStartPlay = true;
    }
    
    public void ClearSave()
    {
        HighscoreManager.Instance.ClearSave();
    }
}
