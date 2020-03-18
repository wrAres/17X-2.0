using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMoveRight : MonoBehaviour
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
        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
    }
}
