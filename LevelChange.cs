using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator animator;
    private int leveltoLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            FadeToNextLevel();
        }
    }

    public void StartFade()
    {
        FadeToNextLevel();
    }


    public void FadetoLevel (int levelIndex)
    {
        leveltoLoad = levelIndex;
        animator.SetTrigger("FADE OUT");
    }

    public void FadeToNextLevel()
    {
        FadetoLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void OnFadeComplete()
    {
        SceneManager.LoadScene(leveltoLoad);
    }

}
