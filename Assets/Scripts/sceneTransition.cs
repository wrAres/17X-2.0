using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneTransition : MonoBehaviour
{
    //[SerializeField] private string newlevel;
	public string scene;
	public bool enterable;
	public bool enterWaterRoom;

	void Start() {
		if (this.gameObject.name.CompareTo("法阵-scene2") == 0)
			enterable = false;
		else 
			enterable = true;
		enterWaterRoom = false;
	}

	void OnTriggerEnter(Collider other){
		if (this.gameObject.name.CompareTo("法阵-scene3") == 0 && !enterWaterRoom) {
			enterWaterRoom = true;
			print("change to scene3: " + enterWaterRoom);
		}

		if (this.gameObject.name.CompareTo("法阵-scene3") == 0 && enterWaterRoom) {
			print("scene 3 directly");
			scene = "scene3";
		}

		if (other.CompareTag("Player") && enterable) {
			Debug.Log("trasferring");
			SceneManager.LoadScene(scene);
		}
	}
}
