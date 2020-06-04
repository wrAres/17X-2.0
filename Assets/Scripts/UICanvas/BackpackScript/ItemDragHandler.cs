using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler {   
    public static bool canPlaceItem = true;
    public static Vector3 previousPosition = new Vector3(0,0,0);
    public static GameObject itemOnGround;
    public static Vector2 originalSize = new Vector2(0f, 0f);
    public static float x;
    public static bool holdItem;

    public GameObject textbox;
    public float itemScale;
    public Text itemName;
    public Vector2 itemOriginalScale;

    public bool dialogShown => FindObjectOfType<TipsDialog>() != null;

    public void OnPointerEnter(PointerEventData eventData) {
        // dispManager.GetComponent<TalismanManager>().DispTextBox(true, element, eventData.position);
        textbox.SetActive(true);
        itemName.text = gameObject.name.ToString();
        textbox.GetComponent<TextboxScaler>().UpdateBoxSize();
        // print("event data: " + this.gameObject.GetComponent<RectTransform>().anchoredPosition);
        textbox.GetComponent<RectTransform>().anchoredPosition = this.gameObject.GetComponent<RectTransform>().anchoredPosition + new Vector2(-80f, -60f);
        transform.localScale *= itemScale;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // dispManager.GetComponent<TalismanManager>().DispTextBox(false, element, eventData.position);
        textbox.SetActive(false);
        transform.localScale /= itemScale;
    }

    public void OnDrag(PointerEventData eventData){
        if (!dialogShown && itemOnGround != null) {
            itemOnGround.transform.position = Input.mousePosition;
            textbox.SetActive(false);
        }
        if(!itemOnGround) return;
        RectTransform item_transform = itemOnGround.GetComponent<RectTransform>();
        string name = itemOnGround.name;
        DragSizes variables = GameObject.Find("Backpack_Roll").GetComponent<DragSizes>();
        if (name.CompareTo("Earth Key") == 0) {
            item_transform.sizeDelta = variables.earthKey;
            // new Vector2(48f, 72f);
        } 
        else if (name.CompareTo("Changable Soil") == 0) {
            item_transform.sizeDelta = variables.dirt;
        }
        else if (name.CompareTo("Heavenly Water") == 0) {
            item_transform.sizeDelta = variables.lifeWater;
        }
        else if (name.CompareTo("Natural Board") == 0) {
            item_transform.sizeDelta = variables.board;
        }
        else if (name.CompareTo("Water Seed") == 0) {
            item_transform.sizeDelta = variables.waterSeed;
        }
        else if (name.CompareTo("Yin-Yang Portal") == 0) {
            item_transform.sizeDelta = variables.earthPortal;
        }
        else if (name.CompareTo("Prime Sun") == 0) {
            item_transform.sizeDelta = variables.glowingSun;
        }
        else if (name.CompareTo("Golden Wood") == 0) {
            item_transform.sizeDelta = variables.firewood;
        }
        else if (name.CompareTo("Taiji Key") == 0) {
            item_transform.sizeDelta = variables.taijiKey;
        }
        else {
            item_transform.sizeDelta = variables.elseSize;
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        // StartCoroutine(Put());
        if (!dialogShown && itemOnGround != null) {
            Put();
            itemOnGround.GetComponent<RectTransform>().anchoredPosition = previousPosition;
            holdItem = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // print("begin drag: "+itemOnGround.name);
        if(!dialogShown) holdItem = true;
        GameObject.Find("playerParticleEffect").GetComponent<castEffect>().castAni();
    }

    // Start is called before the first frame update
    void Start()
    {
        itemOnGround = null;
        holdItem = false;
        itemOriginalScale = transform.localScale;
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
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity) && !dialogShown) {
                GameObject dragOnObject = hitInfo.collider.gameObject;
                int position = ((int)x + 680) / 80;
                // print("drag item position: " + x);
                // print("drag item index: " + position);

                canPlaceItem = Item.canPlace(itemOnGround.name, dragOnObject.name);
                // print(itemOnGround.name + ", on to: " + dragOnObject.name);
                if (canPlaceItem) {
                    if (itemOnGround.name.CompareTo("The Atlas") == 0)
                        transform.localScale = itemOriginalScale / itemScale;
                    else 
                        GameObject.Find("Backpack_Roll").GetComponent<Backpack>().RemoveItem(itemOnGround, position);
                    
                    Item.puzzleEffect(itemOnGround.name, dragOnObject.name, hitInfo.point);
                    if (itemOnGround.name.CompareTo("Tao-Book") != 0 && itemOnGround.name.CompareTo("Talisman") != 0 && itemOnGround.name.CompareTo("The Atlas") != 0)
                        GameObject.Find("pickupEffect").GetComponent<pickupEffect>().castAni(hitInfo.point);
                } else {
                    UISoundScript.PlayWrongSpell();
                    AIDataManager.wrongItemPlacementCount += 1;
                    itemOnGround.GetComponent<RectTransform>().sizeDelta = originalSize;
                    // print(itemOnGround.name + " 's size become: " + originalSize);
                }
                GameObject.Find("playerParticleEffect").GetComponent<castEffect>().stopCasting();
            }
            else {
                print("physics error");
            }
        }
    }
}
