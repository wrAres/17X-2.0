using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static Hashtable itemOnPuzzle;
    public static string[] availablePutList;
    public static SpellTreeManager s;
    public static GameObject flowerpot;

    void Start() {
        s = GameObject.Find("MainUI").GetComponent<SpellTreeManager>();
        // s = FindObjectsOfType<SpellTreeManager>()[0];;
        itemOnPuzzle = new Hashtable();
        itemOnPuzzle.Add("Earth Key", "EarthPortal");
        itemOnPuzzle.Add("Dirt", "River,Flowerpot");
        itemOnPuzzle.Add("Water Seed", "Flowerpot");
        itemOnPuzzle.Add("Firewood", "法阵-scene2");
        itemOnPuzzle.Add("Life Water", "Flowerpot");
        itemOnPuzzle.Add("Glowing Sun", "Flowerpot");
        itemOnPuzzle.Add("Taiji Key", "Water Boss Door");
        itemOnPuzzle.Add("Board", "Background");
        itemOnPuzzle.Add("Earth Portal", "atlasmap2");
        itemOnPuzzle.Add("Taoist Wind", "atlasmap2");
		
    }

    public static bool canPlace(string item, string targetObj) {
        print(item + ", " + targetObj);
        if (item.CompareTo("SpellTreeItem") == 0)
            return true;
        string available = (string)itemOnPuzzle[item];
        if (available == null)
            return false;
        availablePutList = available.Split(',');
        for (int i = 0; i < availablePutList.Length; i++) {
            if (targetObj.CompareTo(availablePutList[i]) == 0){
                if (targetObj.CompareTo("Flowerpot") == 0) {
                    if (item.CompareTo("Dirt") == 0) {
                        if (DontDestroyVariables.growState == 0) {
                            return true;
                        } else {
                            TipsDialog.PrintDialog("Water Seed Grow Order 0");
                            return false;
                        }
                    } else if (item.CompareTo("Water Seed") == 0) {
                        if (DontDestroyVariables.growState == 1) {
                            return true;
                        } else {
                            TipsDialog.PrintDialog("Water Seed Grow Order 1");
                            return false;
                        }
                    } else if (item.CompareTo("Life Water") == 0){
                        if (DontDestroyVariables.growState == 2)
                            return true;
                        else {
                            TipsDialog.PrintDialog("Water Seed Grow Order 2");
                            return false;
                        }
                    } else if (item.CompareTo("Glowing Sun") == 0) {
                        if (DontDestroyVariables.growState == 3) {
                            return true;
                        } else {
                            TipsDialog.PrintDialog("Water Seed Grow Order 3");
                            return false;
                        }
                    }
                } else if (item.CompareTo("Earth Key") == 0) {
                    if (GameObject.Find("EarthPortal").GetComponent<sceneTransition>().openScroll)
                        return true;
                    else return false;
                }
                else {
                    return true;
                }
            }
        }
        TipsDialog.PrintDialog("Wrong Spell");
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

    public static void puzzleEffect(string item, string position, Vector3 hitPoint) {
        if (item.CompareTo("Dirt") == 0 && position.CompareTo("River") == 0){
            GameObject river = GameObject.Find("River");
            // river.GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
            // river .GetComponent<Renderer>().material.color = Color.gray;
            Destroy(river);
            Destroy(GameObject.Find("River Sound 1"));
            Destroy(GameObject.Find("River Sound 2"));
            Destroy(GameObject.Find("River Sound 3"));
            s.UnlockElement(TalisDrag.Elements.WATER);
            
            AIDataManager.UpdateStandardSpellCount("Dirt", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);

            SpellEffectSounds.PlayDirt();
        } 
        else if (item.CompareTo("SpellTreeItem") == 0){
            GameObject.Find("MainUI").GetComponent<Show>().ShowSpelltreeIcon();
            TipsDialog.PrintDialog("Spelltree 1");
            GameObject.Find("Dialog Box").transform.SetSiblingIndex(6);
            DontDestroyVariables.canOpenTalisman = true;
        } 
        else if (item.CompareTo("Earth Key") == 0 && position.CompareTo("EarthPortal") == 0){
            GameObject earthPortal = GameObject.Find("EarthPortal");
            earthPortal.GetComponent<sceneTransition>().enterable = true;
            earthPortal.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/blank_scroll");
        } 
        else if (item.CompareTo("Dirt") == 0 && position.CompareTo("Flowerpot") == 0){
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with dirt");

            AIDataManager.UpdateStandardSpellCount("Dirt", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);

            DontDestroyVariables.growState = 1;
        } 
        else if (item.CompareTo("Board") == 0 && position.CompareTo("Background") == 0){
            GameObject.Find("Background").GetComponent<TileMovement>().CheckDone();
        } 
        else if (item.CompareTo("Water Seed") == 0 && position.CompareTo("Flowerpot") == 0){
            DontDestroyVariables.growState = 2;
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with seed");
        } 
        else if (item.CompareTo("Life Water") == 0 && position.CompareTo("Flowerpot") == 0){
            DontDestroyVariables.growState = 3;
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with bud");

            s.UnlockElement(TalisDrag.Elements.WOOD);

            AIDataManager.UpdateStandardSpellCount("Dirt", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);
            AIDataManager.UpdateStandardSpellCount("Life Water", 1);
            AIDataManager.UpdateStandardSpellCount("water", 3);
        }
        else if (item.CompareTo("Firewood") == 0 && position.CompareTo("法阵-scene2") == 0){
            sceneTransition sceneTrans = GameObject.Find("法阵-scene2").GetComponent<sceneTransition>();
            GameObject.Find("法阵-scene2").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Fire Scroll");
            // sceneTrans.enterable = true;
            s.UnlockElement(TalisDrag.Elements.FIRE);

            AIDataManager.UpdateStandardSpellCount("Firewood", 1);
            AIDataManager.UpdateStandardSpellCount("wood", 3);

            SpellEffectSounds.PlayFire();
        } 
        else if (item.CompareTo("Glowing Sun") == 0 && position.CompareTo("Flowerpot") == 0){
            DontDestroyVariables.growState = 4;
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with Flower and Sun");
            
            GameObject[] mirrorArray = GameObject.Find("mirrors").GetComponent<mirrors>().mirrorArray;
            foreach (GameObject mirror in mirrorArray) {
                mirror.GetComponent<flowerInMirror>().clickable = true;
            }

            GameObject.Find("cameraControl").GetComponent<cameraControl>().active = true;
            TipsDialog.PrintDialog("Zoom In Active");

            AIDataManager.UpdateStandardSpellCount("Glowing Sun", 1);
            AIDataManager.UpdateStandardSpellCount("sun", 2);
            AIDataManager.UpdateStandardSpellCount("earth", 1);
            AIDataManager.UpdateStandardSpellCount("fire", 2);
        } 
        else if (item.CompareTo("Taiji Key") == 0 && position.CompareTo("Water Boss Door") == 0){
            deleteSpellObject("Water Boss Door");

            AIDataManager.gentlypassthedoor = true; 
            AIDataManager.UpdateStandardSpellCount("water", 3);
            AIDataManager.UpdateStandardSpellCount("moon", 1);
            AIDataManager.UpdateStandardSpellCount("sun", 1);
            AIDataManager.UpdateStandardSpellCount("Taiji Key", 1);

            GameObject waterBoss = GameObject.Find("QiangYu");
            //waterBoss.SetActive(false);
        }
        else if (item.CompareTo("Boom") == 0 && position.CompareTo("Water Boss Door") == 0){
            deleteSpellObject("Water Boss Door");

            AIDataManager.gentlypassthedoor = false; 

            // GameObject waterBoss = GameObject.Find("QiangYu");
            // waterBoss.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (item.CompareTo("Taoist Wind") == 0 && position.CompareTo("atlasmap2") == 0){
            GameObject wind = GameObject.Find("WindGroup");
            wind.SetActive(false);

            GameObject.Find("1_water-dark").GetComponent<WaterPool>().timerActivated = true;
            GameObject.Find("6_water-white").GetComponent<WaterPool>().timerActivated = true;
            
            GameObject waterBoss = GameObject.Find("QiangYu");
            waterBoss.GetComponent<waterBoss>().appear();

            TipsDialog.PrintDialog("Water Boss");
        }
        else if (item.CompareTo("Earth Portal") == 0 && position.CompareTo("atlasmap2") == 0){
            GameObject portal = GameObject.Find("WaterToEarthPortal");
            portal.GetComponent<SpriteRenderer>().enabled = true;
            portal.transform.position = hitPoint + new Vector3(0.0f, 0.1f, 0);
        }
        GameObject.Find("Backpack_Roll").GetComponent<Backpack>().RemoveItem(item);
    }
}
