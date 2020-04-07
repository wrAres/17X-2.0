using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler {   
    public static bool canPlaceItem = true;
    public static Vector3 previousPosition = new Vector3(0,0,0);
    public static GameObject itemOnGround;
    public static bool holdItem;

    public GameObject textbox;
    public Text itemName;

    public void OnPointerEnter(PointerEventData eventData) {
        // dispManager.GetComponent<TalismanManager>().DispTextBox(true, element, eventData.position);
        textbox.SetActive(true);
        itemName.text = gameObject.name.ToString();
        // print("event data: " + this.gameObject.GetComponent<RectTransform>().anchoredPosition);
        textbox.GetComponent<RectTransform>().anchoredPosition = this.gameObject.GetComponent<RectTransform>().anchoredPosition + new Vector2(0f, -50f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        // dispManager.GetComponent<TalismanManager>().DispTextBox(false, element, eventData.position);
        textbox.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData){
        itemOnGround.transform.position = Input.mousePosition;
        textbox.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData){
        // StartCoroutine(Put());
        Put();
        itemOnGround.GetComponent<RectTransform>().anchoredPosition = previousPosition;
        holdItem = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // print("begin drag: "+itemOnGround.name);
        holdItem = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        itemOnGround = null;
        holdItem = false;
    }

    // Update is called once per frame
    public void Put()
    {
        if (holdItem) {
            // if (EventSystem.current.IsPointerOverGameObject()) {
            //     Debug.Log("sad return");
            //     return;
            // }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
                GameObject dragOnObject = hitInfo.collider.gameObject;
                canPlaceItem = Item.canPlace(itemOnGround.name, dragOnObject.name);
                if (canPlaceItem) {
                    Item.puzzleEffect(itemOnGround.name, dragOnObject.name, hitInfo.point);
                } else {
                    UISoundScript.PlayWrongSpell();
                    AIDataManager.wrongItemPlacementCount += 1;
                    // print(dragOnObject.name + "'s wrong drop: " + AIDataManager.wrongItemPlacementCount);
                }
            }
            else {
                print("physics error");
            }
        }
    }
}
