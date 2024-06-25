using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class finaltime : MonoBehaviour
{
    [SerializeField] private TMP_Text display;
    private TimeSpan time;

    // Update is called once per frame
    void Update()
    {
        time = Stopwatch.time;
        display.text = "Final Time: " + time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
    }
}
