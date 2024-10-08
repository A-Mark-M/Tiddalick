using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject Mute;
    [SerializeField] GameObject UnMute;

    public static UIController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Mute.SetActive(true);
        UnMute.SetActive(false);
    }

    public void musicMute()
    {
        Mute.SetActive(false);
        UnMute.SetActive(true);
        AudioManager.Instance.musicMute();
        if (AudioManager.Instance.mouseClicks == 1f)
        {
            AudioManager.Instance.PlaySFX("Click");
        }
    }

    public void musicUnMute()
    {
        Mute.SetActive(true);
        UnMute.SetActive(false);
        AudioManager.Instance.musicMute();
        if (AudioManager.Instance.mouseClicks == 1f)
        {
            AudioManager.Instance.PlaySFX("Click");
        }
    }

}