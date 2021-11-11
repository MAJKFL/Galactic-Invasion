using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    Animator fade;
    AsyncOperation async;
    bool waitForLoad;
    float timer;
    float timerWait;

    void Awake()
    {
        fade = GameObject.Find("Fade").GetComponent<Animator>();
    }

    void Start()
    {
        waitForLoad = false;
        timerWait = 1.0f;
    }

    void Update()
    {
        if (waitForLoad)
        {
            timer += Time.deltaTime;
            if(timer >= timerWait)
            {
                waitForLoad = false;
                async.allowSceneActivation = true;
            }
        }
    }

    public void LoadScene(string a)
    {
        Time.timeScale = 1;
        fade.SetBool("show", true);
        waitForLoad = true;
        async = SceneManager.LoadSceneAsync(a);
        async.allowSceneActivation = false;
    }

    public void LoadScene(int a)
    {
        Time.timeScale = 1;
        fade.SetBool("show", true);
        waitForLoad = true;
        async = SceneManager.LoadSceneAsync(/*SceneManager.GetActiveScene().buildIndex + */a);
        async.allowSceneActivation = false;
    }

    public void NextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
}
