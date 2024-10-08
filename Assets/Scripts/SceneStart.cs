using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneStart : MonoBehaviour
{
    public List<GameObject> textObjects = new List<GameObject>();

    private void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 0)
        { FlyoutTextSize.Instance.textSizeButton.SetActive(true); }
        else
        { FlyoutTextSize.Instance.textSizeButton.SetActive(false); }

        foreach (GameObject txt in textObjects)
        {
            if (txt != null)
            {
                // Add the object to the TextManager by name
                TextManager.Instance.AddTextObjectByName(txt.name);
            }
        }

        if (PlayerPrefs.HasKey("text_Size")) TextManager.Instance.LoadTextSize();
        else TextManager.Instance.SetTextSize();

        if (PlayerPrefs.HasKey("font_Style")) TextManager.Instance.LoadFontStyle();
        else TextManager.Instance.SetFontStyle();

        if (PlayerPrefs.HasKey("text_Color")) TextManager.Instance.LoadTextColor();
        else TextManager.Instance.SetTextColor(TextManager.Instance.textColor);
    }
}
