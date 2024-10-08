using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Contents : MonoBehaviour
{

    public static Contents Instance;

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

    public GameObject contentsPanel, contentsCancel;
    [SerializeField] public RectTransform contentsPanelRect;
    [SerializeField] float activeContentsScale, disabledContentsScale;
    [SerializeField] float contentsTweenDuration;
    public Canvas contentsCanvas;

     public async void ContentsCancel()
    {
        AudioManager.Instance.MouseClick();
        await ContentsOutro();
        contentsPanel.SetActive(false);
        contentsCancel.SetActive(false);
    }

    public void ContentsIntro()
    {
        contentsCanvas.sortingOrder = 1;
        contentsPanel.SetActive(true);
        contentsCancel.SetActive(true);
        contentsPanelRect.DOScale(activeContentsScale, contentsTweenDuration);
    }

    async Task ContentsOutro()
    {
        contentsCanvas.sortingOrder = 0;
        await contentsPanelRect.DOScale(disabledContentsScale, contentsTweenDuration)
            .AsyncWaitForCompletion();
    }

    async void GoToPage(int pageNumber)
    {
        AudioManager.Instance.MouseClick();
        await ContentsOutro();
        contentsPanel.SetActive(false);
        contentsCancel.SetActive(false);

        SceneManager.LoadSceneAsync(pageNumber);
    }

}
