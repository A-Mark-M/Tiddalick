using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{

    public void NextPage()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings - 1;
        if (currentScene < sceneCount)
        {
            SceneManager.LoadSceneAsync(currentScene + 1);
            if (AudioManager.Instance.mouseClicks == 1f)
            {
                AudioManager.Instance.PlaySFX("Click");
            }
        }
        else
            UnityEngine.Debug.Log("Last Page");

    }

    public void PreviousPage()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene > 0)
        {
            SceneManager.LoadSceneAsync(currentScene - 1);
            if (AudioManager.Instance.mouseClicks == 1f)
            {
                AudioManager.Instance.PlaySFX("Click");
            }
        }
        else
            UnityEngine.Debug.Log("First Page");
    }
}