using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castEffect : MonoBehaviour
{
    // Start is called before the first frame update
	public bool cast;
	int timer;
	int start;
	public Animator ani;
    void Start()
    {
        cast = false;
		timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
		ani.SetBool("cast",cast);
		timer++;
        if(start +140<=timer && cast ==true){
			cast = false;
		}
		if (Input.GetKey("p")){
			castAni();
		}
    }
	
	void castAni(){
		start = timer;
		cast = true;
	}
}
