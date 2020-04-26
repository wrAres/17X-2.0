﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
	public int isReverse;
	public Animator ani;
	public SystemTree system;
	private bool findFloor;
	public int status;
	public int freeze;
	private bool isWaterScene =>
        SceneManager.GetActiveScene().name == "scene3";
	public bool canAct => !dialogShown && !talismanShown;
	public bool dialogShown =>
        FindObjectOfType<TipsDialog>() != null;
	public bool talismanShown =>
		GameObject.FindGameObjectWithTag("Talisman") != null;
	public bool fall;
	public int startPosition;
	public bool collide;
	Vector3 old_pos;
	bool isMoving;
	int timer;
	void Start()
	{
		system = GameObject.Find("TrigramManager").GetComponent<SystemTree>();
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
		if(timer%30 ==0){
			old_pos = transform.position;
		}
		if(GetComponent<Rigidbody>().velocity.y<-0.1){
			fall=true;
		}else{
			fall=false;
		}
		
		if(GetComponent<Rigidbody>().transform.position.y < -20 ){
			if(startPosition == 0){
				GetComponent<Rigidbody>().transform.position = new Vector3(0,2,-43);
			}else{
				GetComponent<Rigidbody>().transform.position = new Vector3(-3,2,-13);
			}
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			AIDataManager.walkingPuzzleFalls++;
			if (!findFloor) {
				system.FindFloorPiecesTranform();
				findFloor = true;
			}
			system.Execute();
		} 
		
		//ani.SetFloat("Speed", GetComponent<Rigidbody>().velocity.z);
		ani.SetFloat("status",status);
		ani.SetBool("isMoving",isMoving);
		if (canAct&&!fall&&!collide) {
			if (Input.GetKey("w")) {
				GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 3 * isReverse * freeze);
				status = -1 * isReverse;
				isMoving=true;
			}
			else if (Input.GetKey("s")) {
				GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -3 * isReverse * freeze);
				status = 1 * isReverse;
				isMoving = true;
			}
			else if (Input.GetKey("a")) {
				GetComponent<Rigidbody>().velocity = new Vector3(-3 * isReverse * freeze, 0, 0);
				status = 2 * isReverse;
				isMoving = true;
			}
			else if (Input.GetKey("d")) {
				GetComponent<Rigidbody>().velocity = new Vector3(3 * isReverse * freeze, 0, 0);
				status = -2 * isReverse;
				isMoving = true;
			}
			else {
				//status = 0;
				isMoving = false;
			}
			
		}
		
		if (freeze == 1 && isMoving) {
			if (isWaterScene) WaterSoundManagerScript.PlaySound();
			else EarthSoundManager.PlaySound();
		} else {
			if (isWaterScene) WaterSoundManagerScript.StopPlaySound();
			else EarthSoundManager.StopPlaySound();
		}
    }
	
	public void pushBack(){
		Debug.Log(status);
		GetComponent<Rigidbody>().transform.position = old_pos;
		timer = 0;
		collide = true;
		freeze = 0;
		switch(status){
			case -1:
				isMoving = false;
				GetComponent<Rigidbody>().AddForce(0,0,-50);
				Debug.Log(-1);
				break;
			case 1:
				isMoving = false;
				GetComponent<Rigidbody>().AddForce(0,0,50);
				Debug.Log(1);
				break;
			case -2:
				isMoving = false;
				GetComponent<Rigidbody>().AddForce(-50,0,0);
				Debug.Log(-2);
				break;
			case 2:
				isMoving = false;
				GetComponent<Rigidbody>().AddForce(50,0,0);
				Debug.Log(2);
				break;
			default:
				break;
		}
		collide = false;
		freeze = 1;
	}
}







