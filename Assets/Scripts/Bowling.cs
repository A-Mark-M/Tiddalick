using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Bowling : MonoBehaviour
{
    public AudioSource bowling;
    private bool isMuted = true;
    private float bowlingVolume;

    void Awake()
    {
        isMuted = true;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("sfx_Volume"))
        { LoadBowlingVolume(); }
        else
        { SetBowlingVolume(); }

        AudioPanel.Instance.sfxSlider.GetComponent<Slider>().onValueChanged.AddListener(delegate { SetBowlingVolume(); });

        isMuted = true;
    }

    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                if (isMuted)
                {
                    bowling.mute = false;
                    isMuted = false;
                }
            }
            else if (!isMuted)
            {
                bowling.mute = true;
                isMuted = true;
            }
        }
        else if (!isMuted)
        {
            bowling.mute = true;
            isMuted = true;
        }
    }

    public void PlayBowling()
    {
        if (bowling != null)
        {
            bowling.Play();
        }
    }

    public void SetBowlingVolume()
    {
        bowlingVolume = AudioPanel.Instance.sfxSlider.GetComponent<Slider>().value;
        bowling.volume = bowlingVolume;
        PlayerPrefs.SetFloat("sfx_Volume", bowlingVolume);
    }

    public void LoadBowlingVolume()
    {
        AudioPanel.Instance.sfxSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("sfx_Volume");
        SetBowlingVolume();
    }
}