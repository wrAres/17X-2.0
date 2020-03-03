using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHallwayPuzzle : MonoBehaviour
{
    private Vector3 startPos;

    private GameObject MainChar;

    void Start()
    {
        GameObject MainChar = GameObject.Find("Main Character");
        startPos = MainChar.GetComponent<Transform>().position;
    }

    void Update()
    {
        if (GameObject.Find("Main Character").GetComponent<Transform>().position.y < .6) 
        {
            GameObject.Find("Main Character").GetComponent<Transform>().position = startPos; 
        }
    }
}
