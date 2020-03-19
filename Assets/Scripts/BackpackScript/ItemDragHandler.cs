using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{   
    public static bool canPlaceItem = true;
    public static Vector3 previousPosition = new Vector3(0,0,0);
    public static GameObject itemOnGround;
    public static bool holdItem;

    public void OnDrag(PointerEventData eventData){
        itemOnGround.transform.position = Input.mousePosition;
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
                    GameObject imageObj = new GameObject(itemOnGround.name);
                    SpriteRenderer image = imageObj.AddComponent<SpriteRenderer>(); //Add the Image Component script
                    image.sprite = Resources.Load<Sprite>(itemOnGround.name); //Set the Sprite of the Image Component on the new GameObject
                    if (itemOnGround.name.CompareTo("Water Seed") == 0) {
                        imageObj.transform.position = GameObject.Find("Flowerpot").transform.position + new Vector3(0f, 0.8f, 0f);
                    } else if (itemOnGround.name.CompareTo("Glowing Sun") == 0) {
                        imageObj.transform.position = GameObject.Find("Flowerpot").transform.position + new Vector3(0f, 2.0f, 0f);
                    }
                    else {
                        imageObj.transform.position = hitInfo.point + new Vector3(0.0f, 0.1f, 0);
                    }
                    // imageObj.transform.position = hitInfo.point;
                    imageObj.transform.localScale = new Vector3(0.03f, 0.04f, 0.04f);
                    Vector3 temp = imageObj.transform.rotation.eulerAngles;
                    temp.x = 45f;
                    imageObj.transform.rotation = Quaternion.Euler(temp);
                    imageObj.AddComponent<BoxCollider>();
                    imageObj.tag = "SpellObject";
                    // Backpack.backpack.GetComponent<Backpack>().RemoveItem(itemOnGround.name);
                    // Destroy(itemOnGround);
                    Item.puzzleEffect(imageObj.name, dragOnObject.name);
                } else {
                    AIDataManager.wrongItemPlacementCount += 1;
                    print("wrong drop: " + AIDataManager.wrongItemPlacementCount);
                }
            }
            else {
                print("physics error");
            }
        }
    }
}
