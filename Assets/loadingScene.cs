using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void show(){
		GetComponent<Renderer>().enabled = true;
	}
}
