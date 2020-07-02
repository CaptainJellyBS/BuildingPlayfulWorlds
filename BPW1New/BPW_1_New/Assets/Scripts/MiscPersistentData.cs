using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel { Cadet, Lieutenant, Captain, Colonel, General}

public class MiscPersistentData : MonoBehaviour
{
    public static MiscPersistentData Instance { get; private set; }
    public DifficultyLevel currentLevel;
    public string playerName;

    public bool[] levelsAvailable = new bool[5]{ true, false, false, false, false };
    public int[] minScoreForUnlock = new int[5] { 0, 0, 100, 500, 1000 };
    // Start is called before the first frame update


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PersistentData");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Instance = this;
    }
}
