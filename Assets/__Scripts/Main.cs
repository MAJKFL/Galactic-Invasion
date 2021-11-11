using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public bool pause;
    public bool isPlaying = true;
    public SaveData_SO Data;
    GameObject pauseText;
    GameObject menuButton;
    GameObject restartButton;
    Animator victoryPanel;
    Animator defeatPanel;

    void Awake()
    {
        pauseText = GameObject.Find("Pause");
        menuButton = GameObject.Find("MenuButton");
        restartButton = GameObject.Find("RestartButton");
        victoryPanel = GameObject.Find("VictoryPanel").GetComponent<Animator>();
        defeatPanel = GameObject.Find("DefeatPanel").GetComponent<Animator>();
        pauseText.SetActive(false); menuButton.SetActive(false); restartButton.SetActive(false);
    }

    public static Color WhichColor(MonumentColor monC)
    {
        switch (monC)
        {
            case (MonumentColor.Blue):
                return(Color.blue);
                break;
            case (MonumentColor.Green):
                return(Color.green);
                break;
            case (MonumentColor.Red):
                return(Color.red);
                break;
            default:
                return (Color.white);
                break;
        }
    }

    public static bool IsPlaying
    {
        get
        {
            Main main = Camera.main.GetComponent<Main>();
            return main.isPlaying;
        }
        set
        {
            Main main = Camera.main.GetComponent<Main>();
            main.isPlaying = value;
        }
    }
        
    public void EndLevel()
    {
        Data.Unlock(SceneManager.GetActiveScene().buildIndex + 1, true);
        Main.IsPlaying = false;
        victoryPanel.SetBool("show", true);
    }    

    public void defeat()
    {
        Main.IsPlaying = false;
        defeatPanel.SetBool("show", true);
    }

    public void Pause()
    {
        
        if (pause) pause = false;
        else pause = true;
        pauseText.SetActive(pause);
        menuButton.SetActive(pause);
        restartButton.SetActive(pause);
        Main.IsPlaying = !pause;
        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
