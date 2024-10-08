using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System.Diagnostics;

public class FlyoutTextSize : MonoBehaviour
{
    public static FlyoutTextSize Instance;

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

    public GameObject textSizePanel, textSizeButton, textSizeButtonClose, textSizeCancel, sateliteTextSlider;
    [SerializeField] RectTransform textSizePanelRect;
    [SerializeField] float activePosX, disabledPosX;
    [SerializeField] float tweenDuration;
    public Canvas textSizeCanvas;

    public void TextSizeButton()
    {
        AudioManager.Instance.MouseClick();
        textSizePanel.SetActive(true);
        textSizeButton.SetActive(false);
        textSizeButtonClose.SetActive(true);
        textSizeCancel.SetActive(true);
        TextSizePanelIntro();
    }

    public async void TextSizeCancel()
    {
        AudioManager.Instance.MouseClick();
        await TextSizePanelOutro();
        textSizePanel.SetActive(false);
        textSizeButton.SetActive(true);
        textSizeButtonClose.SetActive(false);
        textSizeCancel.SetActive(false);
    }

    void TextSizePanelIntro()
    {
        textSizeCanvas.sortingOrder = 1;
        textSizePanelRect.DOAnchorPosX(activePosX, tweenDuration);
    }

    async Task TextSizePanelOutro()
    {
        textSizeCanvas.sortingOrder = 0;
        await textSizePanelRect.DOAnchorPosX(disabledPosX, tweenDuration).AsyncWaitForCompletion();
    }
}
