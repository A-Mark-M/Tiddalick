using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public void OnPointerEnter(PointerEventData eventData)
    {
       // if (AudioManager.Instance.mouseClicks == 1f)
         //   AudioManager.Instance.PlaySFX("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
