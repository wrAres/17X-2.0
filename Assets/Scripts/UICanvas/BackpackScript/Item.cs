using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item : MonoBehaviour
{
    private static Hashtable itemOnPuzzle;
    public static string[] availablePutList;
    public static SpellTreeManager s;
    public static GameObject flowerpot;
    public static Show dispManager;
    public static TalismanManager talisDisp;
    private static bool spell = false;
    private static string[] groundNames;
    private static bool taoBookOpened = false;

    void Start() {
        s = GameObject.Find("MainUI").GetComponent<SpellTreeManager>();
        // s = FindObjectsOfType<SpellTreeManager>()[0];;
        itemOnPuzzle = new Hashtable();
        itemOnPuzzle.Add("Earth Key", "EarthPortal");
        itemOnPuzzle.Add("Changable Soil", "River,Flowerpot,FutureRock");
        itemOnPuzzle.Add("Water Seed", "Flowerpot");
        itemOnPuzzle.Add("Golden Wood", "法阵-scene2");
        itemOnPuzzle.Add("Heavenly Water", "Flowerpot");
        itemOnPuzzle.Add("Prime Sun", "Flowerpot");
        itemOnPuzzle.Add("Taiji Key", "Water Boss Door");
        itemOnPuzzle.Add("Natural Board", "Background");
        itemOnPuzzle.Add("Yin-Yang Portal", "atlasmap2");
        itemOnPuzzle.Add("Taoism Wind", "Wind Collider");

        talisDisp = GameObject.FindObjectOfType<TalismanManager>();
        dispManager = GameObject.FindObjectOfType<Show>();
    }

    public static void getGroundNames() {
        groundNames = GameObject.FindGameObjectsWithTag("Ground").Select(x => x.name).ToArray();
    }

    public static bool canPlace(string item, string targetObj) {
        spell = false;
        // print(item + ", drop to: " + targetObj);
        if (item.CompareTo("Tao-Book") == 0) {
            taoBookOpened = true;
            return true;
        }
        else if (item.CompareTo("Talisman") == 0 || item.CompareTo("The Atlas") == 0) {
            // if (taoBookOpened)
            //     return true;
            // else {
            //     TipsDialog.PrintDialog("Check Tao Book First");
            //     return false;
            // }
            return true;
        }
        string available = (string)itemOnPuzzle[item];
        if (available == null)
            return false;
        availablePutList = available.Split(',');
        for (int i = 0; i < availablePutList.Length; i++) {
            if (targetObj.CompareTo(availablePutList[i]) == 0){
                if (targetObj.CompareTo("Flowerpot") == 0) {
                    if (item.CompareTo("Changable Soil") == 0) {
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
                    } else if (item.CompareTo("Heavenly Water") == 0){
                        if (DontDestroyVariables.growState == 2)
                            return true;
                        else if (DontDestroyVariables.growState < 2){
                            TipsDialog.PrintDialog("Water Seed Grow Order 2");
                            return false;
                        } else {
                            TipsDialog.PrintDialog("Water Seed Grow Order 2.1");
                            return false;
                        }
                    } else if (item.CompareTo("Prime Sun") == 0) {
                        if (DontDestroyVariables.growState == 3) {
                            return true;
                        } else if (DontDestroyVariables.growState < 3) {
                            TipsDialog.PrintDialog("Water Seed Grow Order 3");
                            return false;
                        } else {
                            TipsDialog.PrintDialog("Water Seed Grow Order 3.1");
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
            // else if (groundNames.Contains(targetObj) && item.CompareTo("Water Seed") != 0 && item.CompareTo("Earth Key") != 0) {
            //     spell = true;
            //     return true;
            // }
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
        if (item.CompareTo("Changable Soil") == 0 && position.CompareTo("FutureRock") == 0){
            GameObject futureRock = GameObject.Find("FutureRock");
            Destroy(futureRock);

            GameObject.Find("Rock10").transform.position = GameObject.Find("Rock5").transform.position + new Vector3(1.8f, 0f, 0f);
            GameObject.Find("Earth Key").transform.position = new Vector3(-1.34f, -1.765f, 3.5f);
            
            AIDataManager.UpdateStandardSpellCount("Changable Soil", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);
        } 
        else if (item.CompareTo("Changable Soil") == 0 && position.CompareTo("River") == 0){
            GameObject river = GameObject.Find("River");
            // river.GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
            // river .GetComponent<Renderer>().material.color = Color.gray;
            Destroy(river);
            Destroy(GameObject.Find("River Sound 1"));
            Destroy(GameObject.Find("River Sound 2"));
            Destroy(GameObject.Find("River Sound 3"));
            s.UnlockElement(TalisDrag.Elements.WATER);
            
            AIDataManager.UpdateStandardSpellCount("Changable Soil", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);

            SpellEffectSounds.PlayDirt();
        } 
        else if (item.CompareTo("Tao-Book") == 0){
            GameObject.Find("MainUI").GetComponent<Show>().ShowSpelltreeIcon();
            TipsDialog.PrintDialog("Spelltree 1");
            GameObject.Find("Dialog Box").transform.SetSiblingIndex(6);
            //DontDestroyVariables.canOpenTalisman = true;
        } 
        else if (item.CompareTo("Talisman") == 0){
            GameObject.Find("MainUI").GetComponent<Show>().ShowTalismanIcon();
            TipsDialog.PrintDialog("Talisman 1");
        } 
        else if (item.CompareTo("The Atlas") == 0){
            // print("talisDisp " + talisDisp == null);
            // print("talisDisp atlas " + talisDisp.atlas == null);
            talisDisp.atlas.SetActive(true);
            dispManager.ToggleIcons(false);
        }
        else if (item.CompareTo("Earth Key") == 0 && position.CompareTo("EarthPortal") == 0){
            GameObject earthPortal = GameObject.Find("EarthPortal");
            earthPortal.GetComponent<sceneTransition>().enterable = true;
            earthPortal.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Earth Portal");
        } 
        else if (item.CompareTo("Changable Soil") == 0 && position.CompareTo("Flowerpot") == 0){
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with dirt");

            AIDataManager.UpdateStandardSpellCount("Changable Soil", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 3);

            DontDestroyVariables.growState = 1;
        } 
        else if (item.CompareTo("Natural Board") == 0 && position.CompareTo("Background") == 0){
            GameObject.Find("Background").GetComponent<TileMovement>().CheckDone();

            AIDataManager.UpdateStandardSpellCount("Natural Board", 1);
            AIDataManager.UpdateStandardSpellCount("earth", 2);
            AIDataManager.UpdateStandardSpellCount("water", 1);
        } 
        else if (item.CompareTo("Water Seed") == 0 && position.CompareTo("Flowerpot") == 0){
            DontDestroyVariables.growState = 2;
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with seed");
        } 
        else if (item.CompareTo("Heavenly Water") == 0 && position.CompareTo("Flowerpot") == 0){
            DontDestroyVariables.growState = 3;
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with bud");

            s.UnlockElement(TalisDrag.Elements.WOOD);

            AIDataManager.UpdateStandardSpellCount("Heavenly Water", 1);
            AIDataManager.UpdateStandardSpellCount("water", 3);
        }
        else if (item.CompareTo("Golden Wood") == 0 && position.CompareTo("法阵-scene2") == 0){
            sceneTransition sceneTrans = GameObject.Find("法阵-scene2").GetComponent<sceneTransition>();
            GameObject.Find("法阵-scene2").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Fire Boss").GetComponent<SpriteRenderer>().enabled = true;

            // sceneTrans.enterable = true;
            s.UnlockElement(TalisDrag.Elements.FIRE);

            AIDataManager.UpdateStandardSpellCount("Golden Wood", 1);
            AIDataManager.UpdateStandardSpellCount("wood", 3);
            AIDataManager.UpdateStandardSpellCount("earth", 1);
            AIDataManager.UpdateStandardSpellCount("wind", 2);
            AIDataManager.UpdateStandardSpellCount("Yin-Yang Portal", 1);

            SpellEffectSounds.PlayFire();

            TipsDialog.PrintDialog("Fire Boss");
        } 
        else if (item.CompareTo("Prime Sun") == 0 && position.CompareTo("Flowerpot") == 0){
            DontDestroyVariables.growState = 4;
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with Flower and Sun");
            
            GameObject[] mirrorArray = GameObject.Find("mirrors").GetComponent<mirrors>().mirrorArray;
            foreach (GameObject mirror in mirrorArray) {
                mirror.GetComponent<flowerInMirror>().clickable = true;
            }

            GameObject.Find("cameraControl").GetComponent<cameraControl>().active = true;
            TipsDialog.PrintDialog("Zoom In Active");

            AIDataManager.UpdateStandardSpellCount("Prime Sun", 1);
            AIDataManager.UpdateStandardSpellCount("sun", 2);
            AIDataManager.UpdateStandardSpellCount("earth", 1);
            AIDataManager.UpdateStandardSpellCount("fire", 2);
        } 
        else if (item.CompareTo("Taiji Key") == 0 && position.CompareTo("Water Boss Door") == 0){
            deleteSpellObject("Water Boss Door");
            GameObject.Find("doorLeft").GetComponent<doorController>().openDoor();
            GameObject.Find("doorRight").GetComponent<doorController>().openDoor();

            AIDataManager.gentlypassthedoor = true; 

            // GameObject waterBoss = GameObject.Find("QiangYu");
            //waterBoss.SetActive(false);
        }
        else if (item.CompareTo("Boom") == 0 && position.CompareTo("Water Boss Door") == 0){
            deleteSpellObject("Water Boss Door");
            GameObject.Find("doorLeft").GetComponent<doorController>().openDoor();
            GameObject.Find("doorRight").GetComponent<doorController>().openDoor();

            AIDataManager.gentlypassthedoor = false; 

            // GameObject waterBoss = GameObject.Find("QiangYu");
            // waterBoss.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (item.CompareTo("Taoism Wind") == 0 && position.CompareTo("Wind Collider") == 0){
            GameObject wind = GameObject.Find("WindGroup");
            wind.SetActive(false);

            DontDestroyVariables.windExist = false;

            AIDataManager.UpdateStandardSpellCount("fire", 1);
            AIDataManager.UpdateStandardSpellCount("wood", 1);
            AIDataManager.UpdateStandardSpellCount("wind", 1);
            AIDataManager.UpdateStandardSpellCount("Taoism Wind", 1);
        }
        else if (item.CompareTo("Yin-Yang Portal") == 0 && position.CompareTo("atlasmap2") == 0){
            GameObject portal = GameObject.Find("WaterToEarthPortal");
            portal.GetComponent<SpriteRenderer>().enabled = true;
            portal.transform.position = hitPoint + new Vector3(0.0f, 0.6f, 0);
        } 
        // else if (spell){
        //     GameObject spell = new GameObject(item);

        //     spell.transform.position = hitPoint + new Vector3(0.0f, 0.1f, 0);;
        //     spell.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //     Vector3 temp = spell.transform.rotation.eulerAngles;
        //     temp.x = 45f;
        //     spell.transform.rotation = Quaternion.Euler(temp);

        //     SpriteRenderer image = spell.AddComponent<SpriteRenderer>(); //Add the Image Component script
        //     image.sprite = Resources.Load<Sprite>("spell/" + item);
        // }
    }
}
