using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMovement : MonoBehaviour
{
    // Start is called before the first frame update
	//public int number;
	public int timer = -50;
	public int  v = 1;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		timer++;
		if(timer<0){
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		}
		if(timer>=0){		
			GetComponent<Rigidbody>().velocity = new Vector3(v,0,0-v);
		}
		if(timer >= 700){
			v *=-1;
			timer = -100;
	    }

    }
}