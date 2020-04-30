using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clouds : MonoBehaviour
{
    // Start is called before the first frame update
	public int number;
	public int timer = 0;
	float leftBound;
	float rightBound;
    void Start()
    {
       
	   number = 1;
	   leftBound = GetComponent<Rigidbody>().transform.position.x+1;
	   rightBound = leftBound+2;
    }

    // Update is called once per frame
    void Update()
    {
		GetComponent<Rigidbody>().velocity = new Vector3(0.3f*number,0,0);
		if(GetComponent<Rigidbody>().transform.position.x > rightBound){
			number = -1;
		}
		if(GetComponent<Rigidbody>().transform.position.x < leftBound){
			number = 1;
		}
		
    }
}
