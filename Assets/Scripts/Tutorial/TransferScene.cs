using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferScene : MonoBehaviour
{
	//[SerializeField] private string newlevel;
	public string scene;
	public bool enterable;

	void Start()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("TutorialWall") && enterable)
		{
			// Debug.Log("trasferring");
			SceneManager.LoadScene(scene);
		}
	}
}