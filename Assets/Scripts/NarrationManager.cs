using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
    public Sound[] narrationSounds;
    public AudioSource narrationSource;
    public GameObject audioNarration, stopNarration;

    private void Start()
    {

        if (PlayerPrefs.HasKey("narration_Volume"))
        {
            LoadNarrationVolume();
        }
        else
        {
            SetNarrationVolume();
        }
        AudioPanel.Instance.narrationSlider.GetComponent<Slider>().onValueChanged.AddListener(delegate { SetNarrationVolume(); });
    }

    void Update()
    {
        if (!narrationSource.isPlaying)
        {
            audioNarration.SetActive(true);
            stopNarration.SetActive(false);
        }
    }

    public void PlayNarration()
    {
        AudioManager.Instance.MouseClick();
        SetNarrationVolume();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        string name = ("Narration " + currentScene);

        Sound s = Array.Find(narrationSounds, x => x.name == name);

        if (s == null)
        {
            UnityEngine.Debug.Log("Sound Not Found");
        }

        else
        {
            audioNarration.SetActive(false);
            stopNarration.SetActive(true);
            narrationSource.PlayOneShot(s.clip);
        }
    }

    public void StopNarration()
    {
        AudioManager.Instance.MouseClick();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        string name = ("Narration " + currentScene);

        Sound s = Array.Find(narrationSounds, x => x.name == name);

        if (s == null)
        {
            UnityEngine.Debug.Log("Sound Not Found");
        }

        else
        {
            audioNarration.SetActive(true);
            stopNarration.SetActive(false);
            narrationSource.Stop();
        }
    }

    public void SetNarrationVolume()
    {
        float narrationVolume = AudioPanel.Instance.narrationSlider.GetComponent<Slider>().value;
        narrationSource.volume = narrationVolume;
        PlayerPrefs.SetFloat("narration_Volume", narrationVolume);
    }

    private void LoadNarrationVolume()
    {
        AudioPanel.Instance.narrationSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("narration_Volume");
        SetNarrationVolume();
    }

    public void ResetNarrationLevels()
    {
        AudioPanel.Instance.narrationSlider.GetComponent<Slider>().value = 0.75f;
        SetNarrationVolume();
    }
}
