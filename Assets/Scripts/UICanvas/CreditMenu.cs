using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditMenu : MonoBehaviour
{
    public static GameObject InCreditMenu;
    public static GameObject Object;
    public static bool GameIsPaused = false;
    private static string scene;

    void Awake()
    {
    	InCreditMenu = GameObject.Find("InCreditMenu");
        InCreditMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
	            if(!GameIsPaused){
	                    InCreditMenu.SetActive(true);
	                    GameIsPaused = true;
	                } else{
	                    InCreditMenu.SetActive(false);
	                    GameIsPaused = false;
	                }
	        
	    }
    }

    public void MainMenu(){
	        Destroy(Object);
	      	GameIsPaused = false;
	      	SceneManager.LoadScene("MainMenu");
	}

	public void Back(){
		SceneManager.LoadScene("MainMenu");
		// Debug.Log("back");
	}
}
