using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Save Data", menuName = "Data", order = 1)]
public class SaveData_SO : ScriptableObject
{
    [Header("Unlocked levels")]
    [SerializeField]
    bool[] unlockedLevels;

    [Header("Unlocked levels")]
    string key;

    public bool IsUnlocked(int a) { return unlockedLevels[a];  }

    public void Unlock(int a, bool b) { if (a < 14) unlockedLevels[a] = b;  }

    void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), this);
    }

    void OnDisable()
    {
        if (key == "")
        {
            key = name;
        }
        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
}
