using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance { get; private set; }
    HighscoreData data;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("HighscoreManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Instance = this;

        data = new HighscoreData(5);

        LoadHighscores();
        Debug.Log(data.ToString());
    }

    public void AddScore(DifficultyLevel lev, string name, int score)
    {
        //Unlock any difficulty levels
        for (int i = 0; i < MiscPersistentData.Instance.levelsAvailable.Length; i++)
        {
            MiscPersistentData.Instance.levelsAvailable[i] = MiscPersistentData.Instance.levelsAvailable[i] || score >= MiscPersistentData.Instance.minScoreForUnlock[i];
        }
        data.AddScore(lev.ToString() + " " + name, score);
    }


    //Saving data code stolen from: https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity#toc-anchor-001
    public void SaveHighscores()
    {
        SaveData saveData = new SaveData();
        saveData.names = data.Names;
        saveData.scores = data.Scores;
        saveData.unlocked = MiscPersistentData.Instance.levelsAvailable;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, saveData);
        file.Close();
    }

    //Loading data code stolen from: https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity#toc-anchor-001
    public void LoadHighscores()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();
            data.Reset();
            for (int i = 0; i < save.names.Length; i++)
            {
                //data.AddScore(save.names[i], save.scores[i]);
                data.Names[i] = save.names[i];
                data.Scores[i] = save.scores[i];
            }

            MiscPersistentData.Instance.levelsAvailable = save.unlocked;
        }

        else
        {
            data.Reset();
            Debug.Log("No game saved!");
            SaveHighscores();
        }
    }

    public void ClearSave()
    {
        data.Reset();
        MiscPersistentData.Instance.levelsAvailable = new bool[] { true, false, false, false, false };
        SaveHighscores();
    }

    public string HighScoreString()
    {
        return data.ToString();
    }
}
