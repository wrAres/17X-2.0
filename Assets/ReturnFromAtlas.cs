using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReturnFromAtlas : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData pointerEventData) {
        Item.talisDisp.atlas.SetActive(false);
        Item.dispManager.ToggleIcons(true);
        Backpack.backpack.GetComponent<Backpack>().AddItem("The Atlas");
    }
}
