using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerInMirror : MonoBehaviour
{
    // Start is called before the first frame update
	public bool isRight;
	public GameObject mirror;
	public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnMouseDown(){
		if(!isRight){
			mirror.GetComponent<mirrors>().reset();
			// AI add wrong selected mirror
		}else{
			player.GetComponent<playerMovement>().isReverse = 1;
			Destroy(mirror);
		}
	}
}
