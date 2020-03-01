using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    // private LinkedList<Item> listOfItems;
    private LinkedList<GameObject> imageObjects;
    private int length;
    public static GameObject backpack;
    public static GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        backpack = GameObject.FindWithTag("Backpack");
        canvas = GameObject.Find("MainUI");
        // listOfItems = new LinkedList<Item>();
        imageObjects = new LinkedList<GameObject>();
        length = 0;
        this.AddItem("branch");
        this.AddItem("dirt");
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
        length++;
        // listOfItems.AddLast(item);

        // string name = item.GetName();
        GameObject imageObj = new GameObject(name); //Create the GameObject
        
        imageObjects.AddLast(imageObj);

        RawImage image = imageObj.AddComponent<RawImage>(); //Add the Image Component script
        ItemDragHandler handler = imageObj.AddComponent<ItemDragHandler>(); //Add item-drag component

        image.texture = Resources.Load<Texture2D>(name); //Set the Sprite of the Image Component on the new GameObject
        imageObj.tag = "Item";

        RectTransform item_transform = imageObj.GetComponent<RectTransform>();
        RectTransform backpack_transform = backpack.GetComponent<RectTransform>();
        item_transform.SetParent(canvas.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel, Canvas/Main UI.

        item_transform.anchoredPosition = backpack_transform.anchoredPosition + new Vector2((length-7.5f)*100, 0);
        item_transform.sizeDelta = new Vector2(60, 60);
        
    }

    public void RemoveItem(string name) {
        length--;
        GameObject itemObject = GameObject.Find(name);
        imageObjects.Remove(itemObject);
        Destroy(itemObject);

        // LinkedList<Item>.Enumerator em = listOfItems.GetEnumerator(); 
        // while (em.MoveNext()) {
        //     Item currItem = em.Current;
        //     if (currItem.GetName().CompareTo(name) == 0) {
        //         listOfItems.Remove(currItem);
        //     }
        // }

    }

    public void Show(bool isShow) {
        backpack.SetActive(isShow);
        LinkedList<GameObject>.Enumerator em = imageObjects.GetEnumerator(); 
        while (em.MoveNext()) {
            GameObject currObj = em.Current;
            currObj.SetActive(isShow);
        }
    }
}
