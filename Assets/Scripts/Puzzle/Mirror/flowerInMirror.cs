using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerInMirror : MonoBehaviour
{
    // Start is called before the first frame update
	public bool isRight;
	public GameObject mirror;
	public GameObject player;
	public Sprite cracked;
	Sprite def;
	int timer;
	bool isCracked;
	int temp;
	public bool clickable = false;
	public bool des;
    void Start()
    {
		def = this.gameObject.GetComponent<SpriteRenderer>().sprite;
		
		timer = 0;
		isCracked = false;
		des = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
		if(isCracked == true && temp+50==timer){
				//this.gameObject.GetComponent<SpriteRenderer>().sprite = def;
				isCracked = false;
				mirror.GetComponent<mirrors>().reset();
				mirror.GetComponent<mirrors>().appear();
		}

		if(des == true && temp+50==timer){
			// Debug.Log("destroy");
			Destroy(mirror);
			
		}
    }
	
	public void ClickMirror(){
		if (clickable) {
			if(!isRight){
				/*this.gameObject.GetComponent<SpriteRenderer>().sprite = cracked;*/
				mirror.GetComponent<mirrors>().disappear();
				temp = timer;
				isCracked = true;
				// Debug.Log("wrong");
				/*if(temp+100<timer){
				this.gameObject.GetComponent<SpriteRenderer>().sprite = def;
				mirror.GetComponent<mirrors>().reset();
				}*/
			}else{
				ClickedCorrectMirror();
				TipsDialog.PrintDialog("Break Mirror");
			}
		} else if (DontDestroyVariables.growState < 3){
			TipsDialog.PrintDialog("Mirror no effect");
		} else {
			TipsDialog.PrintDialog("Mirror need flower");
		}
	}

	public void ClickedCorrectMirror(){
		mirror.GetComponent<mirrors>().crackAll();
		temp = timer;
		des = true;
		player.GetComponent<playerMovement>().isReverse = 1;
		player.GetComponent<playerMovement>().startPosition = 1;
	}

	public void crack(){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = cracked;
	}
	/*void onMouseDown(){
		Debug.Log("click!");
		ClickMirror();
	}*/
		
}