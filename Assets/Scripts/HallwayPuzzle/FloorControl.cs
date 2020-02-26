using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    // Keep a list of platforms the player is colliding with
    public List<string> floor = new List<string>();
    public bool start = false;
    public bool end = false;

    private Vector3 startPos;

    private GameObject randomObj;

    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.Find("Main Character").GetComponent<Transform>().position;
        floor.Clear();
        randomObj = GameObject.Find("PuzzleFloor (1)");
        Destroy(randomObj);
    }

    // Update is called once per frame
    void Update()
    {
        if (floor.Count > 0 && start == false && end == false) start = true;
    }

    public void Push(string obj)
    {
        if (!floor.Contains(obj))
        {
            floor.Add(obj);
        }
    }

    public void Delete(string obj)
    {
        floor.Remove(obj);
    }

    public void CheckIfRestart(GameObject obj)
    {
        // If the list is ever empty after starting restart is the player
        if (start == true && floor.Count <= 0)
        {
            obj.GetComponent<Transform>().position = startPos;
        }
    }

    // Debug method delete later
    private void printList()
    {
        Debug.Log("List size: " + floor.Count);
        string temp = "";
        foreach (string i in floor)
        {
            temp += i + ", ";
        }
        Debug.Log("list is [" + temp + "]");
    }
}
