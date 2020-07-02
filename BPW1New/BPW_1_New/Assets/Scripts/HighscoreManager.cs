using System.Collections;
using System.Collections.Generic;
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

        data.AddScore("Test 1", 19);
        data.AddScore("Test 2", 4);
        data.AddScore("Test 3", 3444);
        data.AddScore("Test 4", 1);
        data.AddScore("Test 5", 43);

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

    //Load and save score here

}
