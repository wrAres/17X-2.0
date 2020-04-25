using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().transform.position = new Vector3(-3,3,-9);
    }

    // Update is called once per frame
    void Update()
    {
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (Input.GetKey("left")&&GetComponent<Rigidbody>().transform.position.x>=-9)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-3, 0, 0);
        }
	if (Input.GetKey("right")&&GetComponent<Rigidbody>().transform.position.x<=4)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(3, 0, 0);
        }
		if(Input.GetKey("up")){
			GetComponent<Rigidbody>().transform.position = new Vector3(-9,2,-8);
		}
		if(Input.GetKey("down")){
			GetComponent<Rigidbody>().transform.position = new Vector3(-3,3,-9);
		}
    }
}
