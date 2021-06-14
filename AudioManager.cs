// If any of this doesn't make sense or doesn't fit something in GMTK 2021, go ahead and edit. I copied this over from GenericGame.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainmenu, soundtrack, explosion1, explosion2, jointLock, gameOver, boost, buttonClick, glassBreak, lowRumble;
    public bool muted = false;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = mainmenu;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            audioSource.clip = lowRumble;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            audioSource.clip = soundtrack;
        }
        audioSource.volume = .1f;
        audioSource.Play();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayOnce(glassBreak);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //MuteToggle();
        }

        if (audioSource != null)
        {
            if (audioSource.mute != muted)
            {
                audioSource.mute = muted;
            }
        }
    }

    public void ButtonSound()
    {
        PlayOnce(buttonClick, audioSource);
    }

    public void GameOver()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = gameOver;
        audioSource.Play();
    }

    public void PlayOnce(AudioClip clip, AudioSource source = null)
    {
        if (clip != null)
        {
            if (source == null)
            {
                source = audioSource;
            }
            source.PlayOneShot(clip, 1f);
        }
    }

    public void PauseToggle(bool state)
    {
        paused = state;
        // silence soundtrack. play pause track. do stupid bool thingoes
        if (paused)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    public void MuteToggle()
    {
        if (muted)
        {
            audioSource.volume = 1f;
            muted = !muted;
            PlayOnce(buttonClick);
        }
        else
        {
            PlayOnce(buttonClick);
            audioSource.volume = 1f;
            muted = !muted;
        }
    }
}
