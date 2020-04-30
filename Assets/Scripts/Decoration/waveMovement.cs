using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveMovement : MonoBehaviour
{
    // Start is called before the first frame update
	public int number;
	public int timer = 0;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
		timer++;
		if(timer >= 240){
			timer = 0;
			number *= -1;
		}
		if(number<0){
			GetComponent<Rigidbody>().velocity = new Vector3(1,0,0);
		}else{
			GetComponent<Rigidbody>().velocity = new Vector3(-1,0,0);
		}
		
    }
}