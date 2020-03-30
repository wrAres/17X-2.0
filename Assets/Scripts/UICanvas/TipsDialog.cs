using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsDialog : MonoBehaviour
{
    public static Text dialogText;
    public TextAsset textFile;
    public static int index;
    public static List<string> textlist = new List<string>();
    public static List<string> textlist2 = new List<string>();

    public static GameObject dialog;
    public static Dictionary<string, string> dialogList;
    public string waterGateDiscription;
    public string riverDiscription;
    public string earthKeyDescription;
    public string talismanDescription;
    public string lobbyPortalDescription;

    /* Where to find what keys are being called:
     * Keys: River, Earth Key - Called in CanvasUI\BackpackScript\PickObject, line 57
     *  Note: ^ These keys use OBJECT names ^
     * Key: lobby       - Called in UICanvas\BackpackScript\PickObject, line 25
     * Key: Walking Puzzle  - Called in UICanvas\BackpackScript\PickObject, line 30
     * Key: Break mirror    - Called in UICanvas\BackpackScript\flowerInMirror, line 49
     * key: click object    - in UICanvas\BackpackScript\PickObject, dialogShow
     */
    void Start() {
        //print("start" + textFile.text);
        var linetext = textFile.text.Split('\n');
        index = 2;
        foreach (var line in linetext) {
                    textlist.Add(line);
         //           print("read " + line );
        }

        dialog = GameObject.Find("Dialog Box");
        dialogText = GameObject.Find("Dialog Text").GetComponent<Text>();
        dialogList = new Dictionary<string, string>();
        dialogList.Add("法阵-scene3", waterGateDiscription);
        dialogList.Add("River", riverDiscription);
        dialogList.Add("Earth Key", earthKeyDescription);
        dialogList.Add("Talisman", talismanDescription);
        dialogList.Add("EarthPortal", lobbyPortalDescription);
        dialog.SetActive(false);
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         NextPage();
    //     }
    // }

    public static bool NextPage() {
        print("index: " + index + ", list length: " + textlist2.Count);
        if (index > textlist2.Count - 1){
            index = 2;
            Debug.Log("no more tips");
            // dialog.SetActive(false);
            return false;
        }
        dialogText.text = textlist2[index].Replace("=", "\n");
        index ++;
        return true;
    }

    public static void PrintDialog(string objName){
        textlist2.Clear();
        if (textlist.Contains(objName)){
            int i = textlist.IndexOf(objName);
            int j = i;
            while(textlist[j].CompareTo("---") != 0 ){
                textlist2.Add(textlist[j]);
                print(textlist[j]);
                j++;
            }
        }
        dialogText.text = textlist2[1].Replace("=", "\n");
        dialog.SetActive(true);
        print("Set dialog active, index: " + index + ", list length: " + textlist2.Count);
    }

    public static void HideTextBox() {
        dialog.SetActive(false);
        print("set dialog false");
    }
}