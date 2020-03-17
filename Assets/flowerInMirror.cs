using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerInMirror : MonoBehaviour
{
    // Start is called before the first frame update
	public bool isRight;
	public GameObject mirror;
	public GameObject player;
	public Sprite cracked;
	Sprite def;
	int timer;
	bool isCracked;
	int temp;
	
    void Start()
    {
		def = this.gameObject.GetComponent<SpriteRenderer>().sprite;
		
		timer = 0;
		isCracked = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
		if(isCracked = true && temp+100==timer){
				this.gameObject.GetComponent<SpriteRenderer>().sprite = def;
				isCracked = false;
				mirror.GetComponent<mirrors>().reset();
				
		}
    }
	
	void OnMouseDown(){
		if(!isRight){
			this.gameObject.GetComponent<SpriteRenderer>().sprite = cracked;
			temp = timer;
			isCracked = true;
			/*if(temp+100<timer){
				this.gameObject.GetComponent<SpriteRenderer>().sprite = def;
				mirror.GetComponent<mirrors>().reset();
			}*/
		}else{
			player.GetComponent<playerMovement>().isReverse = 1;
			Destroy(mirror);
		}
	}
	
	
		
}
