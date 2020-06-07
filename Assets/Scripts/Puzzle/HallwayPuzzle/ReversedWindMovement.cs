using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversedWindMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float XMovement;
    public float YMovement;
    public float ZMovement;
	public GameObject wind;
	float trans;
    void Start()
    {
        trans = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
		if(wind.GetComponent<wind>().boardCast==true){
			GetComponent<SpriteRenderer>().color = new Color(1,1,1,trans);
			trans -= 0.01f;
			Debug.Log("fade");
		}
        if (GetComponent<Rigidbody>().transform.position.z > 15f)
        {
            Vector3 v = GetComponent<Rigidbody>().transform.position;
            v.z -= 14f;
            GetComponent<Rigidbody>().transform.position = v;
        }
        if (GetComponent<Rigidbody>().transform.position.x < -15f)
        {
            Vector3 v = GetComponent<Rigidbody>().transform.position;
            v.x += 25f;
            GetComponent<Rigidbody>().transform.position = v;
        }
        GetComponent<Rigidbody>().velocity = new Vector3(XMovement, YMovement, ZMovement);
    }
}
