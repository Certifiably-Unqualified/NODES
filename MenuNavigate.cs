using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigate : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenuGUI; 
    public GameObject EndScreenGUI;
    public GameObject ActiveGUI;

    [SerializeField]
    private bool gameOver = false;
    [SerializeField]
    ModularPendulumController mpc;
    AudioManager am;

    void Start()
    {
        if (mpc == null && SceneManager.GetActiveScene().buildIndex != 0)
        {
            mpc = GameObject.FindWithTag("Player").GetComponent<ModularPendulumController>();
        }
        if (am == null)
        {
            am = Camera.main.gameObject.GetComponent<AudioManager>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (GamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (mpc != null)
        {
            if (mpc.sticks.Count == 0 && !gameOver)
            {
                gameOver = true;
                StartCoroutine(EndGame());
                StartCoroutine(EndGameMusic());
            }
        }
    }

    public void Resume()
    {
        PauseMenuGUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        PauseMenuGUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        PauseMenuGUI.SetActive(false);
        ActiveGUI.SetActive(true);
        SceneManager.LoadScene(1);
        PlayerPrefs.SetFloat("time", 0);
    }

    IEnumerator EndGame(int increments = 250, float timeToTake = .2f)
    {
        float waitIncrements = timeToTake / increments;
        Time.timeScale = 1f;
        for (int i = increments; i > 0; i--)
        {
            Time.timeScale = (float)i / increments;
            yield return new WaitForSecondsRealtime(waitIncrements);
        }
        Time.timeScale = 0f;
        EndScreenGUI.SetActive(true);
    }
    IEnumerator EndGameMusic(float timeToTake = 1f)
    {
        float waitIncrements = timeToTake / (am.audioSource.volume / 0.1f);
        for (float i = am.audioSource.volume; i > 0f; i-=0.1f)
        {
            yield return new WaitForSecondsRealtime(waitIncrements);
        }
        am.GameOver();
    }
}
