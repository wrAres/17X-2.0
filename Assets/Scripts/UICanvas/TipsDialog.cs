using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsDialog : MonoBehaviour
{
    public static GameObject dialog;
    public static Text dialogText;
    public static Dictionary<string, string> dialogList;
    public string waterGateDiscription;
    public string riverDiscription;
    public string backpackDescription;
    public string spelltreeDescription;
    public string talismanNoticeDescription;
    public string talismanBuildingDescription;
    public string walkingPuzzleDescription;
    public string breakMirrorDescription;
    public string spellTree1description;
    public string spellTree2description;

    /* Where to find what keys are being called:
     * Keys: River, Earth Key - Called in CanvasUI\BackpackScript\PickObject, line 57
     *  Note: ^ These keys use OBJECT names ^
     * Key: Spelltree       - Called in UICanvas\BackpackScript\PickObject, line 25
     * Key: Walking Puzzle  - Called in UICanvas\BackpackScript\PickObject, line 30
     * Key: Break mirror    - Called in UICanvas\BackpackScript\flowerInMirror, line 49
     * key: click object    - in UICanvas\BackpackScript\PickObject, dialogShow
     */
    void Start() {
        dialog = GameObject.Find("Dialog Box");
        dialogText = GameObject.Find("Dialog Text").GetComponent<Text>();
        dialogList = new Dictionary<string, string>();
        dialogList.Add("法阵-scene3", waterGateDiscription);
        dialogList.Add("River", riverDiscription);
        dialogList.Add("Earth Key", backpackDescription);
        dialogList.Add("Spelltree", spelltreeDescription);
        dialogList.Add("Talisman 1", talismanNoticeDescription);
        dialogList.Add("Talisman 2", talismanBuildingDescription);
        dialogList.Add("Walking Puzzle", walkingPuzzleDescription);
        dialogList.Add("Break mirror", breakMirrorDescription);
        dialogList.Add("Spell Tree 1", spellTree1description);
        dialogList.Add("Spell Tree 2", spellTree2description);
        dialog.SetActive(false);
    }

    public static void PrintDialog(string objName){
        if (dialogList.ContainsKey(objName)) {
            dialogText.text = dialogList[objName];
            dialog.SetActive(true);
            print("Set dialog active");
        }
    }

    public static void HideTextBox() {
        dialog.SetActive(false);
        print("set dialog false");
    }
}