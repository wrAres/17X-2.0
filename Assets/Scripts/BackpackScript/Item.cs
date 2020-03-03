using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static Hashtable itemOnPuzzle;
    public static string[] availablePutList;
    public static SpellTreeManager s;

    void Start() {
        s = GameObject.Find("MainUI").GetComponent<SpellTreeManager>();
        // s = FindObjectsOfType<SpellTreeManager>()[0];;
        itemOnPuzzle = new Hashtable();
        itemOnPuzzle.Add("Dirt", "River,Flowerpot");
        itemOnPuzzle.Add("Water Seed", "Dirt");
        itemOnPuzzle.Add("Firewood", "法阵-scene2");
    }

    public static bool canPlace(string item, string position) {
        string available = (string)itemOnPuzzle[item];
        if (available == null)
            return false;
        availablePutList = available.Split(',');
        for (int i = 0; i < availablePutList.Length; i++) {
            if (position.CompareTo(availablePutList[i]) == 0){
                return true;
            }
        }
        return false;
    }
    public static void puzzleEffect(string item, string position, Vector3 newPos) {
        if (item.CompareTo("Dirt") == 0 && position.CompareTo("River") == 0){
            GameObject river = GameObject.Find("River");
            river.GetComponent<CapsuleCollider>().radius = 0;
            GameObject.Find("River").GetComponent<Renderer>().material.color = Color.gray;
            s.UnlockElement(TalisDrag.Elements.WATER);
            Destroy(GameObject.Find("Dirt"));
        } 
        else if (item.CompareTo("Water Seed") == 0 && position.CompareTo("Dirt") == 0){
            GameObject seed = GameObject.Find("Water Seed");
            GameObject sprout = new GameObject("Water Sprout");
            GameObject dirt = new GameObject("Dirt");
            Camera camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            // RectTransform seed_transform = seed.GetComponent<RectTransform>();
            // RectTransform sprout_transform = sprout.GetComponent<RectTransform>();
            // sprout_transform.anchoredPosition = seed_transform.anchoredPosition;
            //sprout.transform.position = camera.ScreenToWorldPoint(seed.transform.position);
            sprout.transform.position = newPos;
            print(seed.transform.position);
            SpriteRenderer image = sprout.AddComponent<SpriteRenderer>(); //Add the Image Component script
            image.sprite = Resources.Load<Sprite>("Water Sprout"); //Set the Sprite of the Image Component on the new GameObject
            sprout.transform.localScale = new Vector3(0.03f, 0.04f, 0.04f);
            Vector3 temp = sprout.transform.rotation.eulerAngles;
            temp.x = 45f;
            sprout.transform.rotation = Quaternion.Euler(temp);
            Destroy(seed);
            s.UnlockElement(TalisDrag.Elements.WOOD);
        } 
        else if (item.CompareTo("Firewood") == 0 && position.CompareTo("法阵-scene2") == 0){
            SceneTransition sceneTrans = GameObject.Find("法阵-scene2").GetComponent<SceneTransition>();
            Debug.Log(sceneTrans);
            sceneTrans.enterable = true;
            s.UnlockElement(TalisDrag.Elements.FIRE);
        } 
        // else if (item.CompareTo("Life Water") == 0 && position.CompareTo("Water Sprout") == 0){
        //     GameObject sspell = GameObject.Find("Life Water");
        //     GameObject sprout = new GameObject("Water Sprout");
        //     sprout.transform.position = seed.transform.position;
        //     SpriteRenderer image = sprout.AddComponent<SpriteRenderer>(); //Add the Image Component script
        //     image.sprite = Resources.Load<Sprite>("Water Sprout"); //Set the Sprite of the Image Component on the new GameObject
        //     sprout.transform.localScale = new Vector3(0.3f, 0.4f, 0.4f);
        //     Vector3 temp = sprout.transform.rotation.eulerAngles;
        //     temp.x = 45f;
        //     sprout.transform.rotation = Quaternion.Euler(temp);
        //     Destroy(seed);
        // }
    }
}
