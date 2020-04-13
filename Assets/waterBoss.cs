using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       /* if(true){
			GetComponent<Renderer>().enabled = true;
		}*/
    }
	
	public void appear(){
	
		GetComponent<Renderer>().enabled = true;
	}
}
