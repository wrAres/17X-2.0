using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    // Start is called before the first frame update
	
	public GameObject player;
	public Rigidbody rb;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Rigidbody>().position.z > 0){
			//player.GetComponent<Rigidbody>().velocity = new Vector3(3,0,0);
			rb.AddForce(10,0,0);
		}
    }
}
