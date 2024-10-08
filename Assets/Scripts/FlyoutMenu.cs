using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class FlyoutMenu : MonoBehaviour
{

    public GameObject menuPanel, menuButton, menuButtonClose, menuCancel;
    [SerializeField] RectTransform menuPanelRect;
    [SerializeField] float activeMenuPosX, disabledMenuPosX;
    [SerializeField] float menuTweenDuration;

    public void MenuButton()
    {
        AudioManager.Instance.MouseClick();
        menuPanel.SetActive(true);
        menuButton.SetActive(false);
        menuButtonClose.SetActive(true);
        menuCancel.SetActive(true);
        MenuPanelIntro();
    }

    public async void MenuCancel()
    {
        AudioManager.Instance.MouseClick();
        await MenuPanelOutro();
        menuPanel.SetActive(false);
        menuButton.SetActive(true);
        menuButtonClose.SetActive(false);
        menuCancel.SetActive(false);
        
    }

    public async void ContentsPage()
    {
        AudioManager.Instance.MouseClick();
        await MenuPanelOutro();
        menuPanel.SetActive(false);
        menuButton.SetActive(true);
        menuButtonClose.SetActive(false);
        menuCancel.SetActive(false);
        Contents.Instance.ContentsIntro();
    }

    public async void TextOptions()
    {
        AudioManager.Instance.MouseClick();
        await MenuPanelOutro();
        menuPanel.SetActive(false);
        menuButton.SetActive(true);
        menuButtonClose.SetActive(false);
        menuCancel.SetActive(false);
        TextPanel.Instance.TextOptionsIntro();
    }

    public async void AudioOptions()
    {
        AudioManager.Instance.MouseClick();
        await MenuPanelOutro();
        menuPanel.SetActive(false);
        menuButton.SetActive(true);
        menuButtonClose.SetActive(false);
        menuCancel.SetActive(false);
        AudioPanel.Instance.AudioOptionsIntro();
    }

    void MenuPanelIntro()
    {
        menuPanelRect.DOAnchorPosX(activeMenuPosX, menuTweenDuration)
            .AsyncWaitForCompletion();
    }

    async Task MenuPanelOutro()
    {
        await menuPanelRect.DOAnchorPosX(disabledMenuPosX, menuTweenDuration)
            .AsyncWaitForCompletion();
    }
}
