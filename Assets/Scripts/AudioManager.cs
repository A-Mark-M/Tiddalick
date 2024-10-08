using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    private NarrationManager narrationManager;
    public float mouseClicks;

    public List<AudioSource> audioSources = new List<AudioSource>(); // List to store AudioSources
    private Dictionary<string, AudioSource> playingSounds = new Dictionary<string, AudioSource>();
    private int maxAudioSources = 10; // Maximum number of AudioSources to use at a time

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { Destroy(gameObject); }
    }

    private void Start()
    {

        narrationManager = FindObjectOfType<NarrationManager>();

        musicSource.mute = false;
        PlayMusic("Background Music");

        if (PlayerPrefs.HasKey("music_Volume"))
        { LoadMusicVolume(); }
        else
        {
            PlayerPrefs.SetFloat("music_Volume", 0.1f);
            LoadMusicVolume();
        }

        if (PlayerPrefs.HasKey("sfx_Volume"))
        { LoadSFXVolume(); }
        else
        {
            PlayerPrefs.SetFloat("sfx_Volume", 0.5f);
            LoadSFXVolume();
        }

        if (PlayerPrefs.HasKey("mouse_Clicks"))
        { LoadMouseClicks(); }
        else
        {
            PlayerPrefs.SetFloat("mouse_Clicks", 1f);
            LoadMouseClicks();
        }

        AudioPanel.Instance.musicSlider.GetComponent<Slider>().onValueChanged.AddListener(delegate { SetMusicVolume(); });
        AudioPanel.Instance.sfxSlider.GetComponent<Slider>().onValueChanged.AddListener(delegate { SetSFXVolume(); });
        AudioPanel.Instance.mouseToggle.GetComponent<Slider>().onValueChanged.AddListener(delegate { SetMouseCliks(); });
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            UnityEngine.Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            UnityEngine.Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }


    public void PlaySFXLong(string soundName)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == soundName);
        if (s != null)
        {
            AudioSource source = GetAvailableAudioSource(); // Your method to get an available AudioSource
            source.clip = s.clip;
            source.Play();
            playingSounds[soundName] = source; // Track the playing sound
        }
        else { UnityEngine.Debug.Log("Sound not found."); }
    }

    public void StopSFXLong(string soundName)
    {
        if (playingSounds.TryGetValue(soundName, out AudioSource source))
        {
            source.Stop();
            playingSounds.Remove(soundName); // Remove from tracking
        }
        else
        {
            UnityEngine.Debug.Log("Sound Not Found: " + soundName);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        // Check if there is an existing AudioSource that isn't currently playing
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        // If no available AudioSource and under the maxAudioSources limit, create a new one
        if (audioSources.Count < maxAudioSources)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();

            // Set the initial volume based on the current SFX volume setting
            float sfxVolume = PlayerPrefs.GetFloat("sfx_Volume", AudioPanel.Instance.sfxSlider.GetComponent<Slider>().value);
            newSource.volume = sfxVolume;

            audioSources.Add(newSource);
            return newSource;
        }

        // No available or new AudioSource
        return null;
    }


public void musicMute()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void SetMusicVolume()
    {
        float musicVolume = AudioPanel.Instance.musicSlider.GetComponent<Slider>().value;
        musicSource.volume = musicVolume;
        PlayerPrefs.SetFloat("music_Volume", musicVolume);
    }

    private void LoadMusicVolume()
    {
        AudioPanel.Instance.musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("music_Volume");
        SetMusicVolume();
    }

    public void SetSFXVolume()
    {
        float sfxVolume = AudioPanel.Instance.sfxSlider.GetComponent<Slider>().value;
        foreach (AudioSource source in audioSources)
        {
            source.volume = sfxVolume;
        }
        sfxSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("sfx_Volume", sfxVolume);
    }

    private void LoadSFXVolume()
    {
        AudioPanel.Instance.sfxSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sfx_Volume");
        SetSFXVolume();
    }

    public void ResetAudioLevels()
    {
        AudioManager.Instance.MouseClick();
        AudioPanel.Instance.musicSlider.GetComponent<Slider>().value = 0.1f;
        SetMusicVolume();
        AudioPanel.Instance.sfxSlider.GetComponent<Slider>().value = 0.5f;
        SetSFXVolume();
        AudioPanel.Instance.mouseToggle.GetComponent<Slider>().value = 1;
    }

    public void MouseClick()
    {
        if (AudioManager.Instance.mouseClicks == 1f)
        {
            AudioManager.Instance.PlaySFX("Click");
        }
    }

    public void SetMouseCliks()
    {
        mouseClicks = AudioPanel.Instance.mouseToggle.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("mouse_Clicks", mouseClicks);
    }
    public void LoadMouseClicks()
    {
        AudioPanel.Instance.mouseToggle.GetComponent<Slider>().value = PlayerPrefs.GetFloat("mouse_Clicks");
        SetMouseCliks();
    }
}