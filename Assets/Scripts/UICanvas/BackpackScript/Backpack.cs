using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    // private LinkedList<Item> listOfItems;
    private GameObject[] imageObjects;
    private int length;
    public static GameObject backpack;
    public static GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        backpack = GameObject.FindWithTag("Backpack");
        canvas = GameObject.Find("MainUI");
        // listOfItems = new LinkedList<Item>();
        imageObjects = new GameObject[9];
        length = 0;
        // this.AddItem("Dirt");
        // this.AddItem("Taiji Key");
        this.AddItem("Glowing Sun");
        this.Show(false);
    }
    void OnGUI() {
        GUI.depth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string name) {
        for (int i = 0; i < length; i++) {
            GameObject currObj = imageObjects[i];
            if (currObj.name.CompareTo(name) == 0) {
                return ;
            }
        }

        GameObject imageObj = new GameObject(name); //Create the GameObject
        
        imageObjects[length] = imageObj;
        length++;

        RawImage image = imageObj.AddComponent<RawImage>(); //Add the Image Component script
        ItemDragHandler handler = imageObj.AddComponent<ItemDragHandler>(); //Add item-drag component

        image.texture = Resources.Load<Texture2D>(name); //Set the Sprite of the Image Component on the new GameObject
        imageObj.tag = "Item";

        RectTransform item_transform = imageObj.GetComponent<RectTransform>();
        RectTransform backpack_transform = backpack.GetComponent<RectTransform>();
        item_transform.SetParent(canvas.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel, Canvas/Main UI.

        item_transform.anchoredPosition = backpack_transform.anchoredPosition + new Vector2((length-7.5f)*100, 0);
        if (name.CompareTo("Life Water") == 0) {
            item_transform.sizeDelta = new Vector2(60, 35);
        } else if (name.CompareTo("Dirt") == 0) {
            item_transform.anchoredPosition = item_transform.anchoredPosition + new Vector2(0, 5);
            item_transform.sizeDelta = new Vector2(45, 40);
        } else if (name.CompareTo("Taiji Key") == 0 || name.CompareTo("Earth Key") == 0) {
            item_transform.sizeDelta = new Vector2(180, 120);
        } else {
            item_transform.sizeDelta = new Vector2(50, 50);
        }
        
        UISoundScript.PlayBackpack();
        imageObj.SetActive(Backpack.backpack.activeSelf);
    }

    public void RemoveItem(string name) {
        int removeIndex = 0;
        GameObject itemObject = null;
        for (int i = 0; i < length; i++) {
            if (name.CompareTo(imageObjects[i].name) == 0) {
                removeIndex = i;
                itemObject = imageObjects[i];
                break;
            }
        }
        for (int i = removeIndex; i < length; i++) {
            imageObjects[i] = imageObjects[i+1];
        }

        length--;
        Destroy(itemObject);

    }

    public void Show(bool isShow) {
        backpack.SetActive(isShow);
        for (int i = 0; i < length; i++) {
            GameObject currObj = imageObjects[i];
            currObj.SetActive(isShow);
        }
    }
}
