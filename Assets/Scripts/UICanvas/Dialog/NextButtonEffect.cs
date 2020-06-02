using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextButtonEffect : MonoBehaviour, IPointerDownHandler
{
    public bool effective = true;
    public void ChangeNextButton() {
        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Next Button shadow");
        if (!effective){
            TipsDialog.nextOnClick = true;
            UISoundScript.PlayDialogNext();
        }
    }

    public void Update() {
        if (effective && Input.GetMouseButtonUp(0)) {
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Next Button");
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData) {
        this.ChangeNextButton();
    }
}
