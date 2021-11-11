using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsLevelUnlocked : MonoBehaviour
{
    public int levelNumber;
    public SaveData_SO Data;
    Button button;
    Image image;
    
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
        if (!Data.IsUnlocked(levelNumber - 1))
        {
            button.enabled = false;
            image.color = Color.grey;
        }
    }
}
