using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class TextPanel : MonoBehaviour
{
    public static TextPanel Instance;

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

    public GameObject textOptionsPanel, textOptionsCancel, textSlider, whiteButton, blackButton, whiteSelected, blackSelected, fontOpen, fontClose, fontCancel, fontDropdown, arialButton, berlinButton, tnrButton;
    [SerializeField] public RectTransform textOptionsPanelRect;
    [SerializeField] public RectTransform fontStylePanelRect;
    [SerializeField] float activeTextOptionsScale, disabledTextOptionsScale;
    [SerializeField] float activeFontStylePosY, disabledFontStylePosY;
    [SerializeField] float textTweenDuration;
    public Canvas textOptionsCanvas;

    public void FontOpen()
    {
        AudioManager.Instance.MouseClick();
        FontStyleIntro();
    }

    public async void FontCancel()
    {
        AudioManager.Instance.MouseClick();
        await FontStyleOutro();

    }

    public async void TextOptionsCancel()
    {
        AudioManager.Instance.MouseClick();
        await TextOptionsOutro();
        textOptionsPanel.SetActive(false);
        textOptionsCancel.SetActive(false);
    }

    public void TextOptionsIntro()
    {
        textOptionsCanvas.sortingOrder = 1;
        textOptionsPanel.SetActive(true);
        textOptionsCancel.SetActive(true);
        textOptionsPanelRect.DOScale(activeTextOptionsScale, textTweenDuration);
    }

    async Task TextOptionsOutro()
    {
        textOptionsCanvas.sortingOrder = 0;
        await textOptionsPanelRect.DOScale(disabledTextOptionsScale, textTweenDuration)
            .AsyncWaitForCompletion();
    }

    public void FontStyleIntro()
    {
        fontDropdown.SetActive(true);
        fontOpen.SetActive(false);
        fontClose.SetActive(true);
        fontCancel.SetActive(true);
        fontStylePanelRect.DOAnchorPosY(activeFontStylePosY, textTweenDuration);
    }

    async Task FontStyleOutro()
    {
        fontDropdown.SetActive(false);
        fontOpen.SetActive(true);
        fontClose.SetActive(false);
        fontCancel.SetActive(false);
        await fontStylePanelRect.DOAnchorPosY(disabledFontStylePosY, textTweenDuration)
            .AsyncWaitForCompletion();
    }

    public void ResetButton()
    {
        TextManager.Instance.ResetText();
    }

}
