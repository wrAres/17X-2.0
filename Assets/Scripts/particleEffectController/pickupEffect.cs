using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupEffect : MonoBehaviour
{
    // Start is called before the first frame update
	public Animator ani;
	public bool cast;
	int timer;
	int startTime;
    void Start()
    {
        cast = false;
		//ani.SetBool("cast",cast);
		timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
		timer++;
		ani.SetBool("cast",cast);
		if(cast==true&&startTime+40 <=timer){
			cast = false;
		}
        if (Input.GetKey("l")){
			castAni();
		}
		if (Input.GetKey("t")){
			stopCasting();
		}
    }
	void castAni(){
		//start = timer;
		cast = true;
		startTime = timer;
	}
	void stopCasting(){
		cast = false;
	}
}
