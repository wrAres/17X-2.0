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
    public static string ToSceneName;
    public static GameObject dialog;
    public static Dictionary<string, string> dialogList;
    public static bool nextOnClick = false;
    public static string Line;
    public static bool startTyping;
    public static bool isTyping = false;
    public static bool printfull = false;
    public float textspeed;
    private static string currDialogRef;
    
    // public string waterGateDiscription;
    // public string riverDiscription;
    // public string talismanDescription;
    // public string lobbyPortalDescription;
    // private static bool dialogOrDesc = true; //true when it is a dialog

    /* Where to find what keys are being called:
     * Keys: River, Earth Key - Called in CanvasUI\BackpackScript\PickObject, line 57
     *  Note: ^ These keys use OBJECT names ^
     * Key: lobby       - Called in UICanvas\BackpackScript\PickObject, line 25
     * Key: Walking Puzzle  - Called in UICanvas\BackpackScript\PickObject, line 30
     * Key: Break mirror    - Called in UICanvas\BackpackScript\flowerInMirror, line 49
     * key: click object    - in UICanvas\BackpackScript\PickObject, dialogShow
     */
    void Awake() {
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
        dialogList.Add("法阵-scene3", "waterGateDiscription");
        dialogList.Add("River", "riverDiscription");
        dialogList.Add("Talisman", "talismanDescription");
        dialogList.Add("EarthPortal", "lobbyPortalDescription");
        dialogList.Add("EarthGround", "EarthGroundDescription");
        dialogList.Add("法阵-scene2", "EarthGroundDescription");
        dialogList.Add("atlasmap2", "EarthGroundDescription");
        dialogList.Add("Baguamap", "EarthGroundDescription");
        dialogList.Add("Earth Key", "EarthGroundDescription");
        dialogList.Add("starground", "EarthGroundDescription");
        dialogList.Add("Moving Puzzle Tip", "EarthGroundDescription");
        /*dialogList.Add("FloorPiece1", "EarthGroundDescription");
        dialogList.Add("FloorPiece2", "EarthGroundDescription");
        dialogList.Add("FloorPiece3", "EarthGroundDescription");
        dialogList.Add("FloorPiece4", "EarthGroundDescription");*/
        dialogList.Add("GameObject", "EarthGroundDescription");
        dialogList.Add("End", "EarthGroundDescription");
        dialogList.Add("Water Seed", "EarthGroundDescription");
        dialogList.Add("Flowerpot", "EarthGroundDescription");
        dialogList.Add("Water Boss Door", "EarthGroundDescription");
        dialogList.Add("6_water-white", "EarthGroundDescription");
        dialogList.Add("1_water-dark", "EarthGroundDescription");
        dialogList.Add("wind", "EarthGroundDescription");
        dialogList.Add("wind (1)", "EarthGroundDescription");
        dialogList.Add("wind (2)", "EarthGroundDescription");
        dialogList.Add("wind (3)", "EarthGroundDescription");
        dialogList.Add("wind (4)", "EarthGroundDescription");
        dialogList.Add("wind (5)", "EarthGroundDescription");
        dialogList.Add("wind (6)", "EarthGroundDescription");
        dialogList.Add("wind (7)", "EarthGroundDescription");
        dialogList.Add("Water Boss", "EarthGroundDescription");
        dialogList.Add("Rock1", "EarthGroundDescription");
        dialogList.Add("Rock2", "EarthGroundDescription");
        dialogList.Add("Rock3", "EarthGroundDescription");
        dialogList.Add("Rock4", "EarthGroundDescription");
        dialogList.Add("TalismanTip", "TalismanTips"); // TODO: Add Talisman Tips to the text file dialogtext 
        dialog.SetActive(false);
    }

     void Update()
     {
        if (startTyping){
            //Stops the previous sentence
            StopAllCoroutines(); 
            StartCoroutine(Typing(Line));
        }
        // type full text if click next
        if (isTyping && printfull ){
                StopAllCoroutines(); 
                dialogText.text = "";
                dialogText.text = Line;
                isTyping = false;
                printfull = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeNextButton();
            // type full text if press space
            if (isTyping){
                StopAllCoroutines(); 
                dialogText.text = "";
                dialogText.text = Line;
                isTyping = false;
            } else if(!NextPage()) {
                dialogText.text = "";
                dialog.SetActive(false);
                if (currDialogRef.CompareTo("Self Introduction") == 0) {
                    //show backpack
                    GameObject.Find("MainUI").GetComponent<Show>().ShowBackpackIcon();
                    Backpack.backpack.GetComponent<Backpack>().Show(true);
                }
            }
        }
        if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) && nextOnClick)
        {
            GameObject.Find("Next Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Next Button");
            nextOnClick = false;
        }
     }

    public static void ChangeNextButton() {
        GameObject nextButton = GameObject.Find("Next Button");
        nextButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Next Button shadow");
        nextOnClick = true;
        UISoundScript.PlayDialogNext();
    }

    public static bool NextPage() {
        // if (dialogOrDesc) {
            // print("index: " + index + ", list length: " + textlist2.Count);
            if (index > textlist2.Count - 1){
                index = 2;
                // Debug.Log("no more tips");
                // dialog.SetActive(false);
                dialogText.text = "";
                GameObject.Find("Next Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Next Button");
                return false;
            }
            Line = textlist2[index].Replace("=", "\n");
            //dialogText.text = "haha";
            startTyping = true;
            index ++;
            return true;
        // }
        // return false;
    }

    public static void PrintDialog(string objName){
        // dialogOrDesc = true;
        currDialogRef = objName;
        textlist2.Clear();
        if (textlist.Contains(objName)){
            int i = textlist.IndexOf(objName);
            int j = i;
            while(textlist[j].CompareTo("---") != 0 ){
                if (textlist[j].CompareTo("Qiang Yu: That is all I have for you:") == 0) {
                    textlist2.Add(textlist[j] + "            " + AIDataManager.DecideTrigram());
                } else {
                    textlist2.Add(textlist[j]);
                }
                // print(textlist[j]);
                j++;
            }
        }
        ToSceneName = objName;
        Line = textlist2[1].Replace("=", "\n");
        //dialogText.text = "haha";
        startTyping = true;
        dialog.SetActive(true);
        // print("Set dialog active, index: " + index + ", list length: " + textlist2.Count);
    }

    // public static void PrintDescription(string objName){
    //     dialogOrDesc = false;
    //     dialogText.text = dialogList[objName];
    //     dialog.SetActive(true);
    // }

    public static void HideTextBox() {
        dialogText.text = "";
        dialog.SetActive(false);
    }

    public static bool CallScene(){
        if ( ToSceneName.CompareTo("Water Boss") == 0) {
            return true;
        } else {
            return false; 
        }
    }
    
    public static void PrintFullDialog(){
        printfull = true;
    }

    IEnumerator Typing(string sentences) {
         startTyping = false;
         isTyping = true;
         dialogText.text = "";
         //print("sentences : " + sentences);
         foreach(char letter in sentences.ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(textspeed);
         }
         isTyping = false; 
    }
}
