using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI YourTime;
    public GameObject ActiveGUI;
    public GameObject EndScreen;
    public GameObject ActivePendulum;


    float startTime;
    string minutes;
    string seconds;

    private void Start()
    {
        startTime = Time.time;
        EndScreen.SetActive(false);
        ActiveGUI.SetActive(true);
        EndScreen.SetActive(false);
    }
    void Update()
    {
        if (ActivePendulum != null)
        {
            float t = Time.time - startTime;
            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f2");
        }
        
        if (ActivePendulum == null)
        {
            EndScreen.SetActive(true);
            ActiveGUI.SetActive(false);
            YourTime.text = "Your Time: " + minutes + ":" + seconds;
        }
        //Debug.Log("CoinNo is: " + CollectSystem.CoinNo);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetFloat("hitbonus", 0);
        //Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(1);
    }
}