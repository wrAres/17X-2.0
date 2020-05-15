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

    public GameObject textbox;
    public Text itemName;

    // Start is called before the first frame update
    void Start()
    {
        backpack = GameObject.FindWithTag("Backpack");
        canvas = GameObject.Find("MainUI");
        // listOfItems = new LinkedList<Item>();
        imageObjects = new GameObject[18];
        length = 0;
        this.AddItem("SpellTreeItem");
        // this.AddItem("Taiji Key");
        // this.AddItem("Glowing Sun");
        this.Show(false);
    }
    void OnGUI() {
        GUI.depth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanAddItem() {
        if(length >= 18) {
            TipsDialog.PrintDialog("Backpack Full");
            return false;
        }
        return true;
    }
    public void AddItem(string name) {
        // for (int i = 0; i < length; i++) {
        //     GameObject currObj = imageObjects[i];
        //     if (currObj.name.CompareTo(name) == 0) {
        //         return ;
        //     }
        // }

        GameObject imageObj = new GameObject(name); //Create the GameObject
        
        imageObjects[length] = imageObj;
        length++;

        RawImage image = imageObj.AddComponent<RawImage>(); //Add the Image Component script
        ItemDragHandler handler = imageObj.AddComponent<ItemDragHandler>(); //Add item-drag component
        imageObj.GetComponent<ItemDragHandler>().textbox = textbox;
        imageObj.GetComponent<ItemDragHandler>().itemName = itemName;

        image.texture = Resources.Load<Texture2D>("spell/" + name); //Set the Sprite of the Image Component on the new GameObject
        imageObj.tag = "Item";

        RectTransform item_transform = imageObj.GetComponent<RectTransform>();
        // RectTransform backpack_transform = backpack.GetComponent<RectTransform>();
        item_transform.SetParent(backpack.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel, Canvas/Main UI.

        item_transform.anchoredPosition = new Vector2((length-12f)*80 + 200, 0);
        if (name.CompareTo("Life Water") == 0) {
            item_transform.sizeDelta = new Vector2(60, 35);
        } else if (name.CompareTo("Dirt") == 0) {
            item_transform.anchoredPosition = item_transform.anchoredPosition + new Vector2(0, 5);
            item_transform.sizeDelta = new Vector2(45, 40);
        } else if (name.CompareTo("Taiji Key") == 0) {
            item_transform.sizeDelta = new Vector2(80, 80);
        } else if (name.CompareTo("Earth Key") == 0) {
            item_transform.sizeDelta = new Vector2(40f, 60f);
        } else if (name.CompareTo("8 Trigram Portal") == 0) {
            item_transform.sizeDelta = new Vector2(48f, 32f);
        } else {
            item_transform.sizeDelta = new Vector2(60f, 60f);
        }
        
        UISoundScript.PlayGetItem();
        imageObj.SetActive(Backpack.backpack.activeSelf);
    }

    public void RemoveItem(GameObject itemObject, int removeIndex) {
        Destroy(itemObject);
        length--;
        for (int i = removeIndex; i < length; i++) {
            imageObjects[i] = imageObjects[i+1];
            // print("move obj " + imageObjects[i].name);
            RectTransform item_tranform = imageObjects[i].GetComponent<RectTransform>();
            item_tranform.anchoredPosition = item_tranform.anchoredPosition + new Vector2(-80, 0);
        }
    }

    public void Show(bool isShow) {
        backpack.SetActive(isShow);
        for (int i = 0; i < length; i++) {
            GameObject currObj = imageObjects[i];
            currObj.SetActive(isShow);
        }
    }
}
