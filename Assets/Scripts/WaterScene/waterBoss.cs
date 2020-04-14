using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBoss : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject BigPool;
	public GameObject SmallPool;
	int timer;
	int temp;
	bool triggered;
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
		timer = 0;
		triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
		timer++;
		if(triggered && timer-60>temp){
			BigPool.GetComponent<WaterPool>().changeColor();
			SmallPool.GetComponent<WaterPool>().changeColor();
			triggered = false;
		}
		if(Input.GetKey("t")){
			appear();
		}
    }
	
	public void appear(){
		triggered = true;
		temp = timer;
		GetComponent<Renderer>().enabled = true;
		
	}
}
