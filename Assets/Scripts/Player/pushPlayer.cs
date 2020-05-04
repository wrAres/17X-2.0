using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnCollisionEnter(Collision collision){
		// Debug.Log("collide!");
		if(collision.gameObject.tag == "Player"){
			//collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			collision.gameObject.GetComponent<playerMovement>().pushBack();
		}
		
	}
}
