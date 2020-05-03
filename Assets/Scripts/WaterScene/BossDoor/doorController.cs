using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    // Start is called before the first frame update
	int open;
	int timer;
	int openTime;
	public Vector3 target;
	public Vector3 axis;
	public int angle;
	public GameObject player;
    void Start()
    {
        open = 0;
		timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
		timer++;
        if (Input.GetKey("o")) {
			openDoor();
		}
		if(open==1&& openTime +250 > timer){
			transform.RotateAround(target, axis, 30 * Time.deltaTime);
		}else if(openTime+250<timer){
			player.GetComponent<playerMovement>().events = false;
		}
    }
	
	
	public void openDoor(){
		open = 1;
		openTime = timer;
		player.GetComponent<playerMovement>().events = true;
	}
}
