using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
    //private bool stopwatchOn;
    private float stopwatchTime;
    [SerializeField] private TMP_Text display;
    [SerializeField] private TMP_Text enemyCounter;
    public GameObject jumpUI;
    public GameObject grappleUI;
    public GameObject defaultUI;
    static GameObject[] enemies;
    public static TimeSpan time;
    // Start is called before the first frame update
    void Start()
    {
        stopwatchTime = 0f;
        defaultUI.SetActive(true);
        jumpUI.SetActive(false);
        grappleUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.beaten || !pauseMenu.paused)
        {
            stopwatchTime += Time.deltaTime;
            itemPicked();
            enemyRemaining();
        }
        time = TimeSpan.FromSeconds(stopwatchTime);
        display.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        
    }
    void itemPicked()
    {
        if (Movement.doubleJumpA)
        {
            defaultUI.SetActive(false);
            jumpUI.SetActive(true);
            grappleUI.SetActive(false);
        }
        if (Movement.grappleAllowed)
        {
            defaultUI.SetActive(false);
            jumpUI.SetActive(false);
            grappleUI.SetActive(true);
        }
    }

    void enemyRemaining()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 0)
        {
            enemyCounter.text = "All Enemies Defeated!";
        }
        else 
        {
            enemyCounter.text = "Enemies Remaining: " + enemies.Length.ToString();
        }
    }
}
