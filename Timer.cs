using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer, endTimer;
    float startTime;
   
    public TextMeshProUGUI score, endScore;
    public float currentscore;
    float hitbonus;

    public GameObject EndScreenGUI;
    public GameObject ActiveGUI;
    public GameObject ActivePendulum;

    void Start()
    {
        startTime = Time.time;
        PlayerPrefs.SetFloat("hitbonus", 0);
        currentscore = -10;
    }

    void Update()
    {
        hitbonus = PlayerPrefs.GetFloat("hitbonus");
        // float t = Time.time - startTime;
        float time = Time.time - startTime;
        currentscore = 2 * time + hitbonus;
        string minutes = ((int)time / 60).ToString();
        string seconds = (time % 60).ToString("f0");
        //string hours = ((int)t / 3600).ToString();

        timer.text = /*hours + ":" + "Time: " +*/ minutes + ":" + seconds;
        score.text = "Score " + currentscore.ToString("f0");
        //timer.text = "Score: " + t / 60;

        //Debug.Log(hitbonus);
        //Debug.Log(currentscore);

        //PlayerPrefs.SetFloat("time", Time.time);
        PlayerPrefs.SetString("minutes", minutes);
        PlayerPrefs.SetString("seconds", seconds);
        PlayerPrefs.SetFloat("currentscore", currentscore);

        endTimer.text = "Time " + timer.text;
        endScore.text = score.text;
    }
}