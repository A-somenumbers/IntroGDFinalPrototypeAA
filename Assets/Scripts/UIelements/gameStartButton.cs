using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStartButton : MonoBehaviour
{
    public int gameSceneStart;

    // Update is called once per frame
    public void startGame()
    {
        SceneManager.LoadScene(gameSceneStart);
    }
}
