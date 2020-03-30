using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPool : MonoBehaviour
{
    // Start is called before the first frame update
	public Sprite darkPool;
	public Sprite bluePool;
	int timer;
	public bool isBlue;
	public bool timerActivated = false;
	
    void Start()
    {
        
		//bluePool = this.gameObject.GetComponent<SpriteRenderer>().sprite;
		timer = 0;
    }

	void Update() {
		if (timerActivated){
			timer++;
			if(timer == 100){
				this.changeColor();
			}
		}
	}
	
	void changeColor(){
		if(isBlue){
			this.gameObject.GetComponent<SpriteRenderer>().sprite = darkPool;
			isBlue = false;
		}else{
			this.gameObject.GetComponent<SpriteRenderer>().sprite = bluePool;
			isBlue = true;
		}
	}
}
