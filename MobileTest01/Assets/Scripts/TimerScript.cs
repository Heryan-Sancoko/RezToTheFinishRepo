using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private Text mTimerText;
    private float secondsPassed;
    private float minutesPassed;
    private float centisecondsPassed = 0;
    private float centisecondsPassedToPrint;
    public bool timerStarted = false;
    private float timePassed = 0;
    public bool stopTimer = false;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        mTimerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (timerStarted && !stopTimer)
        {
            timePassed += Time.deltaTime;
            minutesPassed =  timePassed / 120;
            secondsPassed = timePassed % 60;
            mTimerText.text = minutesPassed.ToString("00") + ":" + secondsPassed.ToString("00.00");

        }

        if (stopTimer == true)
        {
            mTimerText.text = "Speedrun Complete: " + minutesPassed.ToString("00") + ":" + secondsPassed.ToString("00.00") + "\n" + "Press space to try again.";
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void StopTimer()
    {
        stopTimer = true;
    }
}
