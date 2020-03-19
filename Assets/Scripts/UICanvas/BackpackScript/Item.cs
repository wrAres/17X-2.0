using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static Hashtable itemOnPuzzle;
    public static string[] availablePutList;
    public static SpellTreeManager s;
    private static bool waterSeedGrow;
    private static bool dirtInPot;

    void Start() {
        s = GameObject.Find("MainUI").GetComponent<SpellTreeManager>();
        waterSeedGrow = false;
        dirtInPot = false;
        // s = FindObjectsOfType<SpellTreeManager>()[0];;
        itemOnPuzzle = new Hashtable();
        itemOnPuzzle.Add("Earth Key", "EarthPortal");
        itemOnPuzzle.Add("Dirt", "River,Flowerpot");
        itemOnPuzzle.Add("Water Seed", "Flowerpot");
        itemOnPuzzle.Add("Firewood", "法阵-scene2");
        itemOnPuzzle.Add("Life Water", "Water Seed");
        itemOnPuzzle.Add("Glowing Sun", "Water Sprout");
        itemOnPuzzle.Add("Taiji Key", "Water Boss Door");
        itemOnPuzzle.Add("Boom", "Water Boss Door");
        itemOnPuzzle.Add("Board", "Background");
        itemOnPuzzle.Add("Taoist Wind", "atlasmap2");
    }

    public static bool canPlace(string item, string targetObj) {
        print(item + ", " + targetObj);
        string available = (string)itemOnPuzzle[item];
        if (available == null)
            return false;
        availablePutList = available.Split(',');
        for (int i = 0; i < availablePutList.Length; i++) {
            if (targetObj.CompareTo(availablePutList[i]) == 0){
                if (item.CompareTo("Life Water") != 0)
                    return true;
                else if (waterSeedGrow && dirtInPot)
                    return true;
            }
        }
        return false;
    }

    private static void deleteSpellObject(string objName) {
        SpriteRenderer[] spellObjects = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer component in spellObjects){
            if (component.name.CompareTo(objName) == 0) {
                Destroy(component.gameObject);
                return ;
            }
        }
    }

    public static void puzzleEffect(string item, string position) {
        if (item.CompareTo("Dirt") == 0 && position.CompareTo("River") == 0){
            GameObject river = GameObject.Find("River");
            river.GetComponent<CapsuleCollider>().radius = 0;
            GameObject.Find("River").GetComponent<Renderer>().material.color = Color.gray;
            s.UnlockElement(TalisDrag.Elements.WATER);
            
            AIDataManager.UpdateStandardSpellCount("Dirt", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);

            deleteSpellObject("Dirt");
        } 
        else if (item.CompareTo("Earth Key") == 0 && position.CompareTo("EarthPortal") == 0){
            GameObject earthPortal = GameObject.Find("EarthPortal");
            earthPortal.GetComponent<sceneTransition>().enterable = true;
            earthPortal.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Fire Scroll");
            print("Earthportal");
            deleteSpellObject("Earth Key");
        } 
        else if (item.CompareTo("Dirt") == 0 && position.CompareTo("Flowerpot") == 0){
            GameObject flowerpot = GameObject.Find("Flowerpot");
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Flowerpot with dirt");;

            AIDataManager.UpdateStandardSpellCount("Dirt", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);

            dirtInPot = true;

            deleteSpellObject("Dirt");
        } 
        else if (item.CompareTo("Board") == 0 && position.CompareTo("Background") == 0){
            GameObject.Find("Background").GetComponent<TileMovement>().CheckDone();
        } 
        else if (item.CompareTo("Water Seed") == 0 && position.CompareTo("Flowerpot") == 0){
            waterSeedGrow = true;
        } 
        else if (item.CompareTo("Life Water") == 0 && position.CompareTo("Water Seed") == 0){
            GameObject sprout = new GameObject("Water Sprout");
            sprout.transform.position = GameObject.Find("Flowerpot").transform.position + new Vector3(0f, 1.0f, 0f);
            sprout.transform.localScale = new Vector3(0.03f, 0.04f, 0.04f);
            Vector3 temp = sprout.transform.rotation.eulerAngles;
            temp.x = 45f;
            sprout.transform.rotation = Quaternion.Euler(temp);

            SpriteRenderer image = sprout.AddComponent<SpriteRenderer>(); //Add the Image Component script
            image.sprite = Resources.Load<Sprite>("Water Sprout"); //Set the Sprite of the Image Component on the new GameObject
            
            // GameObject sprout = GameObject.Find("Water Sprout");
            // sprout.SetActive(true);

            BoxCollider sproutCollider = sprout.AddComponent<BoxCollider>();
            sproutCollider.size = new Vector3(10f, 10f, 10f);

            DontDestroyVariables.growSprout = true;

            s.UnlockElement(TalisDrag.Elements.WOOD);

            AIDataManager.UpdateStandardSpellCount("Dirt", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);
            AIDataManager.UpdateStandardSpellCount("Life Water", 1);
            AIDataManager.UpdateStandardSpellCount("water", 3);

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("SpellObject"))
                Destroy(obj);
        }
        else if (item.CompareTo("Firewood") == 0 && position.CompareTo("法阵-scene2") == 0){
            GameObject firePortal = GameObject.Find("法阵-scene2");
            sceneTransition sceneTrans = firePortal.GetComponent<sceneTransition>();
            firePortal.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Fire Scroll");
            sceneTrans.enterable = true;
            s.UnlockElement(TalisDrag.Elements.FIRE);

            deleteSpellObject("Firewood");

            AIDataManager.UpdateStandardSpellCount("Firewood", 1);
            AIDataManager.UpdateStandardSpellCount("wood", 3);
        } 
        else if (item.CompareTo("Glowing Sun") == 0 && position.CompareTo("Water Sprout") == 0){
            GameObject bloom = new GameObject("Bloom");
            bloom.transform.position = GameObject.Find("Flowerpot").transform.position + new Vector3(0f, 1.8f, 0f);
            SpriteRenderer image = bloom.AddComponent<SpriteRenderer>(); //Add the Image Component script
            image.sprite = Resources.Load<Sprite>("Bloom"); //Set the Sprite of the Image Component on the new GameObject
            bloom.transform.localScale = new Vector3(0.15f, 0.2f, 0.2f);
            Vector3 temp = bloom.transform.rotation.eulerAngles;
            temp.x = 45f;
            bloom.transform.rotation = Quaternion.Euler(temp);

            AIDataManager.UpdateStandardSpellCount("Glowing Sun", 1);
            AIDataManager.UpdateStandardSpellCount("sun", 2);
            AIDataManager.UpdateStandardSpellCount("earth", 1);
            AIDataManager.UpdateStandardSpellCount("fire", 2);

            deleteSpellObject("Water Sprout");
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
        else if (item.CompareTo("Taiji Key") == 0 && position.CompareTo("Water Boss Door") == 0){
            deleteSpellObject("Water Boss Door");
            deleteSpellObject("Taiji Key");

            AIDataManager.gentlypassthedoor = true; 
            AIDataManager.UpdateStandardSpellCount("water", 3);
            AIDataManager.UpdateStandardSpellCount("moon", 1);
            AIDataManager.UpdateStandardSpellCount("sun", 1);
            AIDataManager.UpdateStandardSpellCount("Taiji Key", 1);

            GameObject waterBoss = GameObject.Find("QiangYu");
            waterBoss.SetActive(false);
        }
        else if (item.CompareTo("Boom") == 0 && position.CompareTo("Water Boss Door") == 0){
            deleteSpellObject("Boom");
            deleteSpellObject("Water Boss Door");

            AIDataManager.gentlypassthedoor = false; 

            GameObject waterBoss = GameObject.Find("QiangYu");
            waterBoss.SetActive(false);
        }
        else if (item.CompareTo("Taoist Wind") == 0 && position.CompareTo("atlasmap2") == 0){
            GameObject wind = GameObject.Find("WindGroup");
            Destroy(wind);

            GameObject waterBoss = GameObject.Find("QiangYu");
            waterBoss.SetActive(true);
        }
    }
}
