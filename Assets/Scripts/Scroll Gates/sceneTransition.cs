using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class sceneTransition : MonoBehaviour
{
    //[SerializeField] private string newlevel;
	public string scene;
	public bool enterable;
	public bool openScroll;
	public Text myText;
	public GameObject loadingScreen;
	void Start() {
		openScroll = false;
		enterable = false;
		if (this.gameObject.name.CompareTo("WaterToEarthPortal") == 0) {
			enterable = true;
		}
		// myText.text = "";
	}

	public void CheckOpen() {
		openScroll = true;
		string name = this.gameObject.name;
		if (name.CompareTo("法阵-scene2") == 0 || name.CompareTo("EarthPortal") == 0)
			enterable = false;
		else 
			enterable = true;
	}

	void OnTriggerEnter(Collider other){

		if (this.gameObject.name.CompareTo("法阵-scene3") == 0 && DontDestroyVariables.enterWaterRoom) {
			// print("scene 3 directly");
			//myText.text = "Loading";
			scene = "scene3";
		}

		if (other.CompareTag("Player") && enterable) {
			// Debug.Log("trasferring");
			//myText.text = "Loading";
			loadingScreen.GetComponent<Renderer>().enabled = true;
			SceneManager.LoadScene(scene);
		}
	}
}
