﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerInMirror : MonoBehaviour
{
    // Start is called before the first frame update
	public bool isRight;
	public GameObject mirror;
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
		}else{
			Destroy(mirror);
		}
	}
}