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
	
    void Start()
    {
        
		//bluePool = this.gameObject.GetComponent<SpriteRenderer>().sprite;
		timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
		if(timer%100 == 0){
			this.changeColor();
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
