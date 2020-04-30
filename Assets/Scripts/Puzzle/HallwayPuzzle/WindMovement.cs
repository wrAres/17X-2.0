using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float XMovement;
    public float YMovement;
    public float ZMovement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().transform.position.z < 1f)
        {
            Vector3 v = GetComponent<Rigidbody>().transform.position;
            v.z += 14f;
            GetComponent<Rigidbody>().transform.position = v;
        }
        if (GetComponent<Rigidbody>().transform.position.x > 10f)
        {
            Vector3 v = GetComponent<Rigidbody>().transform.position;
            v.x -= 25f;
            GetComponent<Rigidbody>().transform.position = v;
        }
        GetComponent<Rigidbody>().velocity = new Vector3(XMovement, YMovement, ZMovement);
    }
}
