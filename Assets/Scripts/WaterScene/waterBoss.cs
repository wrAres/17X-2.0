using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBoss : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject BigPool;
	public GameObject SmallPool;
	int timer;
	int temp;
	bool triggered;
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
		timer = 0;
		triggered = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	public void appear(){
		this.transform.position = new Vector3(-2.56f,1.87f,11.69f);
		triggered = true;
		temp = timer;
		GetComponent<Renderer>().enabled = true;
		
	}
}
