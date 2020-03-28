using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
	public string obj;
    public int index;
    public Text textlable;
   
    List<string> textList = new List<string>();

    void Start()
    {
       obj = GameObject.Find("Dialog Text").GetComponent<Text>().text;
       print(obj);
        index = 0;
        print("get text");
    }
    
    //private void OnEnable(){
      //  textlable.text = textList[index];
      //  index ++;
    //}

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count){
            index = 0;
            print("end key");
            return;
        }
        if(Input.GetKeyDown(KeyCode.R))
        { 
            print ("get key R");
        	textlable.text = textList[index];
        	index++;
        }
    }

    void GotText(string obj)
    {
    	textList.Clear();
    	index = 0;

    	string[] lineDate = null;
    	int i = obj.Length / 10;
    	int y = obj.Length % 10;
    	if (i < 1 && (i == 1 && y ==0 ))
    	{
    		lineDate[0] = obj;
    	//} else if (i == 1 && y ==0 ){
    		//lineDate[0] = obj;
    	} else
    	{
    		int x = 0;
    		if (y <1){
    			i -=1;
    		}
			for (int j = 0; j < i; ++j){
				obj = obj.Insert(x+9,"\n");
				x += 9;
    		}
    		lineDate = obj.Split('\n');
    	}

    	foreach(var line in lineDate){
    		textList.Add(line);
    	}

  
    }
}




