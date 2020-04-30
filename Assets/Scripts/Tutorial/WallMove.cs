using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move() 
    {
        // Debug.Log("moved");
        GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);
    }
}
