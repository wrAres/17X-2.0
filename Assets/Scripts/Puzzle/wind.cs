using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    // Start is called before the first frame update
	
	public GameObject player;
	public Rigidbody rb;
	public bool boardCast;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
		boardCast = false;
    }

    // Update is called once per frame
    void Update()
    {
		 if(Input.GetKeyDown("p")){
		 	windFadeOut();
		 }
        if(player.GetComponent<Rigidbody>().position.z > 0){
			//player.GetComponent<Rigidbody>().velocity = new Vector3(3,0,0);
			rb.AddForce(5,0,-4);
		}
    }
	
	public void windFadeOut(){
		boardCast = true;
	}
}
