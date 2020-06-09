using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private GameObject buttonImage;

    public void Start() {
        buttonImage = this.gameObject.transform.GetChild(0).gameObject;
    }
    public void OnPointerEnter(PointerEventData pointerEventData) {
        buttonImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Menu/" + buttonImage.name + "_Shadow");
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        buttonImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Menu/" + buttonImage.name);
    }

}
