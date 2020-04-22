using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clouds : MonoBehaviour
{
    // Start is called before the first frame update
	public int number;
	public int timer = 0;
	int range;
    void Start()
    {
       range = Random.Range(800,1500);
	   number = 1;
    }

    // Update is called once per frame
    void Update()
    {
		timer++;
		if(timer >= range){
			timer = 0;
			number *= -1;
		}
		if(number<0){
			GetComponent<Rigidbody>().velocity = new Vector3(0.1f,0,0);
		}else{
			GetComponent<Rigidbody>().velocity = new Vector3(-0.1f,0,0);
		}
		
    }
}
