using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isTimerRunning = true;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            float timeElapsed = Time.time - startTime;
            DisplayTime(timeElapsed);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimerForSeconds(float stopTime)
    {
        StartCoroutine(StopTimerCoroutine(stopTime));
    }

    private System.Collections.IEnumerator StopTimerCoroutine(float stopTime)
    {
        isTimerRunning = false;
        yield return new WaitForSeconds(stopTime);
        isTimerRunning = true;
        startTime = Time.time - (Time.time - startTime); 
    }
}
