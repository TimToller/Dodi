using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float timeRemaining = 30;
    public Text timeText;
    public Text timeText2;
    public Image fillImg;
    public float timeAmt;

    private bool timerIsRunning = false;

    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        startTimer();
        timeAmt = timeRemaining;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if(timeText!=null||timeText2!=null)
                {
                    DisplayTime(timeRemaining);
                }
                if(fillImg!=null)
                {
                    fillImg.fillAmount = timeRemaining / timeAmt;
                }
            }
            else
            {
                Debug.Log("Time is up");
                timeRemaining = 0;
                timerIsRunning = false;
                if(sceneName==null)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
        }
    }

    public void startTimer()
    {
        timerIsRunning = true;
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
