using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockOnGround : MonoBehaviour
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
		this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY|
																			RigidbodyConstraints.FreezeRotationX|
																			RigidbodyConstraints.FreezeRotationZ|
																			RigidbodyConstraints.FreezePositionX|
																			RigidbodyConstraints.FreezePositionZ|
																			RigidbodyConstraints.FreezeRotationY;
	}
}
