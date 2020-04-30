using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDoor : MonoBehaviour
{
    // Start is called before the first frame update
	private int timer;
	public bool isFreezed;
	public GameObject player;
	public Sprite front;
	public Sprite back;
	public Sprite left;
	public Sprite right;
    void Start()
    {
        timer = 0;
		isFreezed = false;
    }

    // Update is called once per frame
    void Update()
    {
        int status = player.GetComponent<playerMovement>().status;
		switch(status){
			case 1:
				this.gameObject.GetComponent<SpriteRenderer>().sprite = back;
				break;
			case 2:
				this.gameObject.GetComponent<SpriteRenderer>().sprite = left;
				break;
			case -1:
				this.gameObject.GetComponent<SpriteRenderer>().sprite = front;
				break;
			case -2:
				this.gameObject.GetComponent<SpriteRenderer>().sprite = right;
				break;
		}
		if(isFreezed == true){
			timer++;
			if(timer == 50){
				player.GetComponent<playerMovement>().freeze = 1;
				isFreezed = false;
				timer = 0;
			}
		}
    }
	
	void OnTriggerEnter(Collider other){
	
		other.GetComponent<playerMovement>().isReverse = -1;
		other.GetComponent<playerMovement>().freeze = 0;
		other.GetComponent<Rigidbody>().transform.position = new Vector3(0,2,-43);
		
		isFreezed = true;
		
	}
	
}
