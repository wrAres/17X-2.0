using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
     SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame(){
     UnityEditor.EditorApplication.isPlaying = false;
     Debug.Log("quit game");
     Application.Quit();
    }
}