using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneTransition : MonoBehaviour
{
    //[SerializeField] private string newlevel;
	public string scene;
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			Debug.Log("trasferring");
			SceneManager.LoadScene(scene);
	    }
	}
}
