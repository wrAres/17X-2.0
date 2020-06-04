﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    // Start is called before the first frame update
	private Vector3 dy;
	//private Vector3 dz;
    void Start()
    {
        dy = new Vector3(0f,-0.4f,0.5f);
		//dz = (0,0,-0.3f);
    }

    // Update is called once per frame
    void Update()
    {
		float cameraY = this.transform.localPosition.y;
		float cameraZ = this.transform.localPosition.z;
        if(Input.GetAxis("Mouse ScrollWheel")>0f &&cameraY <= 1.8f ){
			this.transform.position += dy;
			// Debug.Log(cameraY);
		}
		
		if(Input.GetAxis("Mouse ScrollWheel")<0f && cameraY>= -4.0f ){
			this.transform.position -= dy;
			// Debug.Log(cameraY);
		}
    }
}
