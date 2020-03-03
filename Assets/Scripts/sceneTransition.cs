using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
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
		if(other.CompareTag("Player") && enterable){
			Debug.Log("trasferring");
			SceneManager.LoadScene(scene);
	    }
	}
}
