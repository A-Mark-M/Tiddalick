using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class AudioPanel : MonoBehaviour
{
    public static AudioPanel Instance;

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

    public GameObject audioOptionsPanel, audioOptionsCancel, musicSlider, narrationSlider, sfxSlider, mouseToggle;
    [SerializeField] public RectTransform audioOptionsPanelRect;
    [SerializeField] float activeAudioOptionsScale, disabledAudioOptionsScale;
    [SerializeField] float audioTweenDuration;
    public Canvas audioOptionsCanvas;

    public async void AudioOptionsCancel()
    {
        AudioManager.Instance.MouseClick();
        await AudioOptionsOutro();
        audioOptionsPanel.SetActive(false);
        audioOptionsCancel.SetActive(false);
    }

    public void AudioOptionsIntro()
    {
        audioOptionsCanvas.sortingOrder = 1;
        audioOptionsPanel.SetActive(true);
        audioOptionsCancel.SetActive(true);
        audioOptionsPanelRect.DOScale(activeAudioOptionsScale, audioTweenDuration);
    }

    async Task AudioOptionsOutro()
    {
        audioOptionsCanvas.sortingOrder = 0;
        await audioOptionsPanelRect.DOScale(disabledAudioOptionsScale, audioTweenDuration)
            .AsyncWaitForCompletion();
    }
}
