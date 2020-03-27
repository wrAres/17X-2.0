using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneTransition : MonoBehaviour
{
    //[SerializeField] private string newlevel;
	public string scene;
	public bool enterable;
	public Sprite icon0;
	public Sprite icon1;
	private int status;
	void Start() {
		if (this.gameObject.name.CompareTo("法阵-scene2") == 0 || this.gameObject.name.CompareTo("EarthPortal") == 0)
			enterable = false;
		else 
			enterable = true;
		
		status = 0;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = icon0;
	}

	void OnTriggerEnter(Collider other){

		if (this.gameObject.name.CompareTo("法阵-scene3") == 0 && DontDestroyVariables.enterWaterRoom) {
			print("scene 3 directly");
			scene = "scene3";
		}

		if (this.gameObject.name.CompareTo("法阵-scene3") == 0 && !DontDestroyVariables.enterWaterRoom) {
			DontDestroyVariables.enterWaterRoom = true;
			print("change to scene3: " + DontDestroyVariables.enterWaterRoom);
		}

		if (other.CompareTag("Player") && enterable) {
			Debug.Log("trasferring");
			SceneManager.LoadScene(scene);
		}
	}
	
	void changeSprite(){
		if(status == 0){
			status = 1;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = icon1;
		}else{
			status = 0;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = icon0;
		}
	}
	
	
}
