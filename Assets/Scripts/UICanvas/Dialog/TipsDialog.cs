using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsDialog : MonoBehaviour
{
    public bool isPaused => FindObjectOfType<Show>().lockGame;

    public static Text dialogText;
    public TextAsset textFile;
    public static int index;
    public static List<string> textlist = new List<string>();
    public static List<string> textlist2 = new List<string>();
    public static string ToSceneName;
    public static GameObject dialog;
    public static List<string> dialogList;
    public static bool nextOnClick = false;
    public static int ditto = 0;
    public static string Line;
    public static bool startTyping;
    public static bool isTyping = false;
    public static bool printfull = false;
    public float textspeed;
    private static string currDialogRef;
    public static GameObject nextButton;

    public static GameObject Option;
    // store option name(instrad of using GetOption)
    public static string GetOption;
    public static List<string> OptionList = new List<string>();
    public static Text OptionAText;
    public static Text OptionBText;
    public static Text OptionCText;
    public static Text OptionDText;
    public static bool getOption = false;
    public static bool pickOption = false;
    public static bool introAppear = false;

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
        Option = GameObject.Find("Options");
        OptionAText = GameObject.Find("OptionA Text").GetComponent<Text>();
        OptionBText = GameObject.Find("OptionB Text").GetComponent<Text>();
        OptionCText = GameObject.Find("OptionC Text").GetComponent<Text>();
        OptionDText = GameObject.Find("OptionD Text").GetComponent<Text>();

        nextButton = GameObject.Find("Next Button");
        nextButton.GetComponent<NextButtonEffect>().effective = false;

        dialogList = new List<string>();
        dialogList.Add("法阵-scene3");
        dialogList.Add("River");
        dialogList.Add("Talisman");
        dialogList.Add("EarthPortal");
        dialogList.Add("EarthGround");
        dialogList.Add("法阵-scene2");
        dialogList.Add("atlasmap2");
        dialogList.Add("Baguamap");
        dialogList.Add("Earth Key");
        dialogList.Add("starground");
        dialogList.Add("Moving Puzzle Tip");
        dialogList.Add("GameObject");
        dialogList.Add("End");
        dialogList.Add("Water Seed");
        dialogList.Add("Flowerpot");
        dialogList.Add("Water Boss Door");
        dialogList.Add("6_water-white");
        dialogList.Add("1_water-dark");
        dialogList.Add("wind");
        dialogList.Add("wind (1)");
        dialogList.Add("wind (2)");
        dialogList.Add("wind (3)");
        dialogList.Add("wind (4)");
        dialogList.Add("wind (5)");
        dialogList.Add("wind (6)");
        dialogList.Add("wind (7)");
        dialogList.Add("Water Boss");
        dialogList.Add("Water Boss 2");
        dialogList.Add("Rock1");
        dialogList.Add("Rock2");
        dialogList.Add("Rock3");
        dialogList.Add("Rock4");
        dialogList.Add("TalismanTip"); // TODO: Add Talisman Tips to the text file dialogtext 
        dialogList.Add("Walking Puzzle Hint 1");
        dialogList.Add("Walking Puzzle Hint 2");
        dialogList.Add("Walking Puzzle Hint 3");
        dialogList.Add("Walking Puzzle Hint 4");
        dialogList.Add("rockInWaterRoom");
        dialogList.Add("Ditto Mirror");
        dialogList.Add("Fire Seed");
        dialogList.Add("Cold Fire Seed");

        dialog.SetActive(false);
        dialog.SetActive(false);
        Option.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Space) && !isPaused && !pickOption)
        {
            nextButton.GetComponent<NextButtonEffect>().ChangeNextButton();
            // type full text if press space
            if (isTyping){
                StopAllCoroutines(); 
                dialogText.text = "";
                dialogText.text = Line;
                isTyping = false;
            } else if(!NextPage()) {
                dialogText.text = "";
                dialog.SetActive(false);
                nextOnClick = false;
                CheckCurrentTipForNextMove();
            }
        }
        if(!pickOption){
	        if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) && nextOnClick && !isPaused)
	        {
	            GameObject.Find("Next Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Next Button");
	            nextOnClick = false;
	        }
	    }
     }

    public static void CheckCurrentTipForNextMove() {
        if (currDialogRef.CompareTo("Self Introduction") == 0) {
            //show backpack
            GameObject.Find("MainUI").GetComponent<Show>().ShowBackpackIcon();
            Backpack.backpack.GetComponent<Backpack>().Show(true);
        } else if (currDialogRef.CompareTo("Fire Boss") == 0) {
            GameObject.Find("法阵-scene2").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Fire Boss").GetComponent<SpriteRenderer>().enabled = false;
        } else if (CallScene()) {
            GameObject.Find("WaterSoundManager").GetComponent<TransferToCredit>().Transfer();
        }
    }

    public static bool NextPage() {
       
         // if (dialogOrDesc) {
              //print("index: " + index + ", list length: " + textlist2.Count);
	     	//if (index == textlist2.Count && textlist2[textlist2.Count - 1].CompareTo("Qiang Yu: Interesting... Fighting against your fate, please =don’t let me down, human...") == 0){	
	    // } else {
            // To make sure the conditions for activating multiple choice are met
        	if (index < textlist2.Count - 1 && textlist2[index].CompareTo("Qiang Yu: And I have some questions for you:") == 0){
                //Active option button
                Option.SetActive(true);
                nextButton.SetActive(false);
                pickOption = true;
                PrintOptions();
                return true;      	
	    	} else if (index > textlist2.Count - 1){
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
        //}
        // return false;
    }

    public static void PrintDialog(string objName){
        if (objName.CompareTo("Self Introduction") == 0 && introAppear) {
            return ;
        }
        // dialogOrDesc = true;
        currDialogRef = objName;
        textlist2.Clear();
        if (textlist.Contains(objName)){
            int j = textlist.IndexOf(objName);
            //int j = i;
            while(textlist[j].CompareTo("---") != 0 ){
                if (textlist[j].CompareTo("Ditto board copied") == 0) {
                    textlist2.Add("Ditto board copied " + ditto + "th mirror.");
                } else if (textlist[j].CompareTo("Qiang Yu: That is all I have for you:") == 0) {
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

    public static void ToggleTextBox(bool show) {
        if(!show || (show && dialogText.text != "")) {
            dialog.SetActive(show);
        }
        if(show && dialogText.text != "") {
            isTyping = false;
            dialogText.text = Line;
        }
    }

    public static bool CallScene(){
        if ( ToSceneName.CompareTo("Water Boss 2") == 0) {
            return true;
        } else {
            return false; 
        }
    }
    
    public static void PrintFullDialog(){
        printfull = true;
    }
 
    public static void PlayOption(string Name){
    	// store option name
        if (Name.CompareTo("OptionA") == 0){
            OptionList.Add("A");
        } else if (Name.CompareTo("OptionB") == 0){
            OptionList.Add("B");
        } else if (Name.CompareTo("OptionC") == 0){
            OptionList.Add("C");
        } else if (Name.CompareTo("OptionD") == 0){
            OptionList.Add("D");
        }
        //reset
        if (index == textlist2.Count-1 && textlist2[index].CompareTo("Me") == 0){
            getOption = false;
            Option.SetActive(false);
            nextButton.SetActive(true);
            pickOption = false;
            index = 2;
            print(OptionList[0] + OptionList[1] + OptionList[2] +OptionList[3] );
            PrintDialog("Water Boss 2");
        //next option
        } else{
             PrintOptions();
        }
    }

    public static void PrintOptions(){
            index++;
            dialogText.text = textlist2[index];
            OptionAText.text = textlist2[++index];
            OptionBText.text = textlist2[++index];
            OptionCText.text = textlist2[++index];
            OptionDText.text = textlist2[++index];
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
