using System.Collections;
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

	private bool isWaterScene =>
        SceneManager.GetActiveScene().name == "scene3";


	void Start()
    {
		system = GameObject.Find("TrigramManager").GetComponent<SystemTree>();
		findFloor = false;
    }

    // Update is called once per frame
    void Update()
    {
		
		if(GetComponent<Rigidbody>().transform.position.y < -20){
			GetComponent<Rigidbody>().transform.position = new Vector3(0,2,-43);
			AIDataManager.walkingPuzzleFalls++;
			if (!findFloor) {
				system.FindFloorPiecesTranform();
				findFloor = true;
			}
			system.Execute();
		} 
		
		ani.SetFloat("Speed", GetComponent<Rigidbody>().velocity.z);
		if(Input.GetKey("w")){
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,3*isReverse);
			if(isWaterScene) WaterSoundManagerScript.PlaySound();
		}
		else if(Input.GetKey("s")){
			GetComponent<Rigidbody>().velocity = new Vector3(0,0,-3*isReverse);
			if (isWaterScene) WaterSoundManagerScript.PlaySound();
		}
        else if(Input.GetKey("a")){
			GetComponent<Rigidbody>().velocity = new Vector3(-3*isReverse,0,0);
			if (isWaterScene) WaterSoundManagerScript.PlaySound();
		}
		else if(Input.GetKey("d")){
			GetComponent<Rigidbody>().velocity = new Vector3(3*isReverse,0,0);
			if (isWaterScene) WaterSoundManagerScript.PlaySound();
		}
		else {
			if (isWaterScene) WaterSoundManagerScript.StopPlaySound();
		}
    }
}
