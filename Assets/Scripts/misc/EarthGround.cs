using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthGround : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject player;
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
			collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY|
																			RigidbodyConstraints.FreezeRotationX|
																			RigidbodyConstraints.FreezeRotationZ|
																			RigidbodyConstraints.FreezeRotationY;
			TipsDialog.PrintDialog("Self Introduction");
			TipsDialog.introAppear = true;
			GameObject.Find("MainUI").GetComponent<SpellTreeManager>().UnlockElement(TalisDrag.Elements.EARTH);
		}
		
	}
}
