using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneTransition : MonoBehaviour
{
    //[SerializeField] private string newlevel;
	public string scene;
	public bool enterable;

	void Start() {
		if (this.gameObject.name.CompareTo("法阵-scene2") == 0)
			enterable = false;
		else 
			enterable = true;
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
}
