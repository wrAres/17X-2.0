﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
	public int isReverse;
	public float dx=0,dy=0,dz=0;
	public Animator ani;
	private bool findFloor;
	public int status;
	public int freeze;
	private bool isWaterScene =>
        SceneManager.GetActiveScene().name == "scene3";
	public bool canAct => !dialogShown && !talismanShown && !shineIcon && camera.enabled &&!events&&!fall&&!FindObjectOfType<Show>().lockGame;
	//public bool canAct => !events&&!fall;
	public bool dialogShown =>
        FindObjectOfType<TipsDialog>() != null;
	public bool talismanShown =>
		GameObject.FindGameObjectWithTag("Talisman") != null;
	public bool shineIcon =>
        GameObject.Find("DarkBackground").GetComponent<LeaveIconBright>().shine == true;
	public bool fall;
	public int startPosition;
	public bool collide;
	public Camera camera;
	int direction;
	bool cooldown;
	int recorder;
	Vector3 old_pos;
	bool isMoving;
	int timer;
	public float speed;
	public bool events;
	public Vector3 checkpoint;
	public int pushbackForce;
	void Start()
	{
		findFloor = false;
		status = 0;
		freeze = 1;
		startPosition = 0;
		fall = false;
		isMoving = false;
		old_pos = transform.position;
		timer = 0;
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		collide = false;
		recorder = 0;
		cooldown = false;
		events = false;
		direction = 0;
		checkpoint = new Vector3(8.56f,0.763f,-61.032f);
		
    }

    // Update is called once per frame
    void Update()
    {
		/*if(old_pos != transform.position){
			isMoving = true;
		}else{
			isMoving = false;
		}*/
		timer++;
		//GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		if(recorder + 20 <= timer && collide == true){
			collide = false;
			cooldown = false;
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		}
		if(timer%30 ==0){
			old_pos = transform.position;
		}
		if(GetComponent<Rigidbody>().velocity.y<-0.1){
			
			fall=true;
			status = 0;
			isMoving = false;
		}else{
			fall=false;
			
		}
		float yPos = Mathf.Clamp(GetComponent<Rigidbody>().transform.position.y,0.8f,0.8f);
		if(GetComponent<Rigidbody>().velocity.y >= 0.1){
			GetComponent<Rigidbody>().AddForce(0,-50,0);
		}
		if(GetComponent<Rigidbody>().transform.position.y < -20 ){
			/*if(startPosition == 0){
				GetComponent<Rigidbody>().transform.position = new Vector3(8.56f, 0.763f, -61.31f);
			}else{
				GetComponent<Rigidbody>().transform.position = new Vector3(-3,2,-13);
			}*/
			GetComponent<Rigidbody>().transform.position = checkpoint;
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			AIDataManager.walkingPuzzleFalls++;
			// if (!findFloor) {
			// 	system.FindFloorPiecesTranform();
			// 	findFloor = true;
			// }
		} 
		if(canAct == false){
			isMoving = false;
		}
		//ani.SetFloat("Speed", GetComponent<Rigidbody>().velocity.z);
		ani.SetFloat("status",status);
		ani.SetBool("isMoving",isMoving);
		if (canAct&&!fall&&!collide) {
			if (Input.GetKeyUp("w")||Input.GetKeyUp("a")||Input.GetKeyUp("s")||Input.GetKeyUp("d")){
				GetComponent<Rigidbody>().velocity = new Vector3(0+dx,0+dy,0+dz);
			}
			if (Input.GetKey("w")) {
				direction = -1 * isReverse;
				status = -1 * isReverse;
				isMoving=true;
				if(Input.GetKey("a")){
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f*speed * isReverse* freeze+dx, 0+dy, 0.75f*speed * isReverse * freeze+dz);
					status = 2 * isReverse;
					direction = -3* isReverse;
				}else if(Input.GetKey("d")){
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f*speed * isReverse * freeze+dx, 0, 0.75f * speed * isReverse * freeze+dz);
					status = -2 * isReverse;
					direction = 4* isReverse;
				}else {
					GetComponent<Rigidbody>().velocity = new Vector3(0+dx, 0, 1 * speed * isReverse * freeze+dz);
				}
			}
			else if (Input.GetKey("s")) {
				direction = 1 * isReverse;
				status = 1 * isReverse;
				isMoving = true;
				if(Input.GetKey("a")){
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f*speed * isReverse* freeze+dx, 0, -0.75f*speed * isReverse * freeze+dz);
					status = 2 * isReverse;
					direction = -4* isReverse;
				}else if(Input.GetKey("d")){
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f*speed * isReverse * freeze+dx, 0, -0.75f * speed * isReverse * freeze+dz);
					status = -2 * isReverse;
					direction = 3* isReverse;
				}else {
					GetComponent<Rigidbody>().velocity = new Vector3(0+dx, 0, -1 * speed * isReverse * freeze+dz);
				}
			}
			else if (Input.GetKey("a")) {
				direction = 2 * isReverse;
				status = 2 * isReverse;
				isMoving = true;
				if(Input.GetKey("w")){
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f*speed * isReverse* freeze+dx, 0, 0.75f*speed * isReverse * freeze+dz);
					direction = -3* isReverse;
				}else if(Input.GetKey("s")){
					GetComponent<Rigidbody>().velocity = new Vector3(-0.75f*speed * isReverse * freeze+dx, 0, -0.75f * speed * isReverse * freeze+dz);
					direction = -4* isReverse;
				}else {
					GetComponent<Rigidbody>().velocity = new Vector3(-1 * speed * isReverse * freeze+dx, 0, 0+dz);
				}
			}
			else if (Input.GetKey("d")) {
				//GetComponent<Rigidbody>().velocity = new Vector3(3 * isReverse * freeze, 0, 0);
				direction = -2 * isReverse;
				status = -2 * isReverse;
				isMoving = true;
				if(Input.GetKey("w")){
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f*speed * isReverse* freeze+dx, 0, 0.75f*speed * isReverse * freeze+dz);
					direction = 4* isReverse;
				}else if(Input.GetKey("s")){
					GetComponent<Rigidbody>().velocity = new Vector3(0.75f*speed * isReverse* freeze+dx, 0, -0.75f*speed * isReverse * freeze+dz);
					direction = 3* isReverse;
				}else {
					GetComponent<Rigidbody>().velocity = new Vector3(1 * speed * isReverse * freeze+dx, 0, 0);
				}
			}
			else {
				//status = 0;
				isMoving = false;
			}
			
		}
		
		
		if (freeze == 1 && canAct && isMoving) {
			if (isWaterScene) WaterSoundManagerScript.PlaySound();
			else EarthSoundManager.PlaySound();
		} else {
			if (isWaterScene) WaterSoundManagerScript.StopPlaySound();
			else EarthSoundManager.StopPlaySound();
		}
    }
	
	public void pushBack(){
		if(cooldown == true){
			return;
		}else{
			cooldown = true;
		}
		// Debug.Log(status);
		
		
		collide = true;
		
		recorder = timer;
		switch(direction){
			case -1:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1*pushbackForce);
				//GetComponent<Rigidbody>().AddForce(0,0,-1*pushbackForce);
				// Debug.Log(-1);
				break;
			case 1:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(0, 0, pushbackForce);
				//GetComponent<Rigidbody>().AddForce(0,0,pushbackForce);
				// Debug.Log(1);
				break;
			case -2:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(-1*pushbackForce, 0, 0);
				//GetComponent<Rigidbody>().AddForce(-1*pushbackForce,0,0);
				// Debug.Log(-2);
				break;
			case 2:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(pushbackForce, 0, 0);
				//GetComponent<Rigidbody>().AddForce(pushbackForce,0,0);
				// Debug.Log(2);
				break;
			case -3:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(0.6f*pushbackForce,0,-0.6f*pushbackForce);
				//GetComponent<Rigidbody>().AddForce(0.6f*pushbackForce,0,-0.6f*pushbackForce);
				break;
			case 3:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(-0.6f*pushbackForce,0,0.6f*pushbackForce);
				//GetComponent<Rigidbody>().AddForce(-0.6f*pushbackForce,0,0.6f*pushbackForce);
				break;
			case -4:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(0.6f*pushbackForce,0,0.6f*pushbackForce);
				//GetComponent<Rigidbody>().AddForce(0.6f*pushbackForce,0,0.6f*pushbackForce);
				break;
			case 4:
				isMoving = false;
				GetComponent<Rigidbody>().velocity = new Vector3(-0.6f*pushbackForce,0,-0.6f*pushbackForce);
				//GetComponent<Rigidbody>().AddForce(-0.6f*pushbackForce,0,-0.6f*pushbackForce);
				break;
			default:
				break;
		}
		
	}
}







