using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBoard : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject player;
	bool isContact = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isContact==true){
			player.GetComponent<playerMovement>().dx = this.GetComponent<Rigidbody>().velocity.x;
			player.GetComponent<playerMovement>().dz = this.GetComponent<Rigidbody>().velocity.z;
		}
    }
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Player"){
		/*	other.gameObject.GetComponent<playerMovement>().dx = this.GetComponent<Rigidbody>().velocity.x;
			other.gameObject.GetComponent<playerMovement>().dz = this.GetComponent<Rigidbody>().velocity.z;*/
			isContact = true;
		}
	}
	void OnCollisionExit(Collision other){
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<playerMovement>().dx = 0;
			other.gameObject.GetComponent<playerMovement>().dz = 0;
			isContact = false;
		}
	}
}
