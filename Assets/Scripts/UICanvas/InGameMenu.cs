using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour
{
    public static GameObject GameMenu;
    public static GameObject Object;
    public static bool GameIsPaused = false;
    public static List<GameObject> GameObjectList;
    public static string sceneName;
    private void Awake()
    {
      GameMenu = GameObject.Find("InGameMenu");
      Object = GameObject.Find("MainUI");
      GameMenu.SetActive(false);
    }

	void Update()
	    {
	      // if (SceneManager.GetActiveScene().name == "MainMenu") {
	      //   GameObject.Find("MainUI").SetActive(false);
	      // }
	      if(Input.GetKeyDown(KeyCode.Escape)){
	            if(!GameIsPaused){
	                    GameMenu.SetActive(true);
	                    GameIsPaused = true;
	                } else{
	                    GameMenu.SetActive(false);
	                    GameIsPaused = false;
	                }
	        }
	    }

	    public static void QuitGame(){
	      //  UnityEditor.EditorApplication.isPlaying = false;
	       Debug.Log("quit game");
	       Application.Quit();
	    }
	    public static void Control() {
	       	GameIsPaused = false;
	       	GameMenu.SetActive(false);
	       	Debug.Log("Control");
	    }

	    public static void MainMenu(){
	    	//remove dontdestroyonload
	    	Destroy(Object); 
	      	SceneManager.LoadScene("MainMenu");
	    }
	    public static void Option(){
	       	Debug.Log("Option");
	       	GameMenu.SetActive(false);
	       	GameIsPaused = false;
	    }
	}