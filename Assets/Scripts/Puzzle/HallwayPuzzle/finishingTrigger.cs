using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.tag == "Player"){
			obj.GetComponent<playerMovement>().startPosition = 1;
		}
    }
}
