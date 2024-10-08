using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    public List<TextMeshProUGUI> textObjects = new List<TextMeshProUGUI>();
    public TMP_FontAsset[] fonts;
  

    private int textSize;
    private int fontStyle;
    private string fontName;
    public Color textColor;

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
        if (PlayerPrefs.HasKey("text_Size")) LoadTextSize();
        else
        {
            PlayerPrefs.SetInt("Text_Sixe", 30);
            PlayerPrefs.Save();
            LoadTextSize();
        }

        if (PlayerPrefs.HasKey("font_Style")) LoadFontStyle();
        else
        {
            PlayerPrefs.SetInt("font_Style", 1);
            PlayerPrefs.SetString("font_Name", "Berlin Sans FB");
            PlayerPrefs.Save();
            LoadFontStyle();
        }

        if (PlayerPrefs.HasKey("text_Color")) LoadTextColor();
        else
        {
            PlayerPrefs.SetString("text_Color", "white");
            PlayerPrefs.Save();
            LoadTextColor();
        }

        TextPanel.Instance.textSlider.GetComponent<Slider>().onValueChanged.AddListener(delegate { UpdateSatelitePanel(); });
        FlyoutTextSize.Instance.sateliteTextSlider.GetComponent<Slider>().onValueChanged.AddListener(delegate { UpdateTextPanel(); });
    }

    public void AddTextObjectByName(string objectName)
    {
        CleanTextObjectList();

        TextMeshProUGUI[] allTextObjects = FindObjectsOfType<TextMeshProUGUI>(true);

        foreach (TextMeshProUGUI tmp in allTextObjects)
        {
            if (tmp.gameObject.name == objectName && !textObjects.Contains(tmp))
            {
                textObjects.Add(tmp);
                break;
            }
        }
    }

    private void UpdateTextPanel()
    {
        TextPanel.Instance.textSlider.GetComponent<Slider>().value = (int)FlyoutTextSize.Instance.sateliteTextSlider.GetComponent<Slider>().value;
        SetTextSize();
    }
    private void UpdateSatelitePanel()
    {
        FlyoutTextSize.Instance.sateliteTextSlider.GetComponent<Slider>().value = (int)TextPanel.Instance.textSlider.GetComponent<Slider>().value;
        SetTextSize();
    }

    // Sets the text size for all text objects in the list
    public void SetTextSize()
    {
        CleanTextObjectList();
        textSize = (int)TextPanel.Instance.textSlider.GetComponent<Slider>().value;

        // Update the font size of all Text components in the list
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            if (textObject != null)
                textObject.fontSize = textSize;
        }

        // Save the text size to PlayerPrefs
        PlayerPrefs.SetInt("text_Size", textSize);
        PlayerPrefs.Save();
    }

    // Load the saved text size and apply it to all text objects
    public void LoadTextSize()
    {
        CleanTextObjectList();
        TextPanel.Instance.textSlider.GetComponent<Slider>().value = PlayerPrefs.GetInt("text_Size");
        FlyoutTextSize.Instance.sateliteTextSlider.GetComponent<Slider>().value = PlayerPrefs.GetInt("text_Size");
        SetTextSize();
    }

    
    public void ResetText()
    {
        AudioManager.Instance.MouseClick();

        // Set text font to default (Berlin Sans FB)
        fontStyle = 1;
        fontName = "Berlin Sans FB";
        PlayerPrefs.SetInt("font_Style", 1);
        PlayerPrefs.SetString("font_Name", "Berlin Sans FB");
        PlayerPrefs.Save();
        SetFontStyle();

        // Set text color to default (white)
        TextPanel.Instance.whiteSelected.SetActive(true);
        TextPanel.Instance.blackSelected.SetActive(false);
        PlayerPrefs.SetString("text_Color", "white");
        SetTextColor(Color.white);

        // Set text size to default (30)
        TextPanel.Instance.textSlider.GetComponent<Slider>().value = 30f;
        SetTextSize();

    }

    public void SetArial()
    {
        fontStyle = 0;
        fontName = "Arial";
        PlayerPrefs.SetInt("font_Style", 0);
        PlayerPrefs.SetString("font_Name", "Arial");
        PlayerPrefs.Save();
        SetFontStyle();
        TextPanel.Instance.FontCancel();
    }


    public void SetBerlin()
    {
        fontStyle = 1;
        fontName = "Berlin Sans FB";
        PlayerPrefs.SetInt("font_Style", 1);
        PlayerPrefs.SetString("font_Name", "Berlin Sans FB");
        PlayerPrefs.Save();
        SetFontStyle();
        TextPanel.Instance.FontCancel();
    }


    public void SetTimesNewRoman()
    {
        fontStyle = 2;
        fontName = "Times New Roman";
        PlayerPrefs.SetInt("font_Style", 2);
        PlayerPrefs.SetString("font_Name", "Times New Roman");
        PlayerPrefs.Save();
        SetFontStyle();
        TextPanel.Instance.FontCancel();
    }

    public void SetFontStyle()
    {
        EnsureTextObjectsPopulated();
        int s = fontStyle;
        string n = fontName;
        TextPanel.Instance.fontOpen.GetComponentInChildren<TextMeshProUGUI>().text = n;
        TextPanel.Instance.fontOpen.GetComponentInChildren<TextMeshProUGUI>().font = fonts[s];
        TextPanel.Instance.fontClose.GetComponentInChildren<TextMeshProUGUI>().text = n;
        TextPanel.Instance.fontClose.GetComponentInChildren<TextMeshProUGUI>().font = fonts[s];

        // Update the font style of all Text components in the list
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            if (textObject != null)
                textObject.font = fonts[s];
        }
    }

    public void LoadFontStyle()
    {
        EnsureTextObjectsPopulated();
        fontStyle = PlayerPrefs.GetInt("font_Style");
        fontName = PlayerPrefs.GetString("font_Name");
        SetFontStyle();
    }

    public void TextWhite()
    {
        AudioManager.Instance.MouseClick();
        TextPanel.Instance.whiteSelected.SetActive(true);
        TextPanel.Instance.blackSelected.SetActive(false);
        PlayerPrefs.SetString("text_Color", "white");
        SetTextColor(Color.white);
    }

    public void TextBlack()
    {
        AudioManager.Instance.MouseClick();
        TextPanel.Instance.whiteSelected.SetActive(false);
        TextPanel.Instance.blackSelected.SetActive(true);
        PlayerPrefs.SetString("text_Color", "black");
        SetTextColor(Color.black);
    }

    public void SetTextColor(Color color)
    {
        EnsureTextObjectsPopulated();
        textColor = color;
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            if (textObject != null)
            {
                textObject.color = color;
            }
        }

        if (textColor == Color.white)
        { PlayerPrefs.SetString("text_Color", "white"); }
        if (textColor == Color.black)
        { PlayerPrefs.SetString("text_Color", "black"); }
    }

    public void LoadTextColor()
{
        EnsureTextObjectsPopulated();
        if (PlayerPrefs.HasKey("text_Size"))
    {
        if (PlayerPrefs.GetString("text_Color") == "white")
        { textColor = Color.white; }

        if (PlayerPrefs.GetString("text_Color") == "black")
        { textColor = Color.black; }
    }
    SetTextColor(textColor);
}

    public void EnsureTextObjectsPopulated()
    {
        CleanTextObjectList(); // Remove any null entries

        // Get the textObjects list from SceneStart
        SceneStart sceneStart = FindObjectOfType<SceneStart>();
        if (sceneStart != null)
        {
            // Iterate through SceneStart's textObjects list and populate the TextManager's textObjects list
            foreach (GameObject obj in sceneStart.textObjects)
            {
                if (obj != null)
                {
                    TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
                    if (tmp != null && !textObjects.Contains(tmp))
                    {
                        textObjects.Add(tmp);
                    }
                }
            }
        }
    }

    private void CleanTextObjectList()
    {
        textObjects.RemoveAll(item => item == null);  // Remove any missing (null) items
    }
}
