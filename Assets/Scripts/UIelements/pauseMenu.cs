using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject PauseMenu;
    public GameObject LevelBeatMenu;
    public GameObject UI;
    public GameObject failmenu;
    public static bool beaten = false;
    // Update is called once per frame
    private void Start()
    {
        PauseMenu.SetActive(false);
        LevelBeatMenu.SetActive(false);
        failmenu.SetActive(false);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !beaten)
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (killPlayer.dead  == true)
        {
            failmenu.SetActive(true);
            UI.SetActive(false);
        }

        if (beaten)
        {
            LevelBeatMenu.SetActive(true);
            UI.SetActive(false );
        }
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        UI.SetActive(false);
        Time.timeScale = 0f;
        paused = true;

    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void mainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        paused = false;
        beaten = false;
        PauseMenu.SetActive(false);
        LevelBeatMenu.SetActive(false);
    }
    public void quit()
    {
        Application.Quit();
    }

    public void restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        killPlayer.dead = false;
        failmenu.SetActive(false);
        PauseMenu.SetActive(false);
        LevelBeatMenu.SetActive(false);
        paused = false;
        beaten = false;
        UI.SetActive(true );
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1f;
        

    }

    public void nextLevel()
    {
        Time.timeScale = 1.0f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
        paused = false;
        beaten = false;
        PauseMenu.SetActive(false);
        LevelBeatMenu.SetActive(false);
    }
    public void levelSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
        paused = false;
        beaten = false;
        PauseMenu.SetActive(false);
        LevelBeatMenu.SetActive(false);
    }
}
