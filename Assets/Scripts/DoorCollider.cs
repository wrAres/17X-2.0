using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject portal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(portal.GetComponent<sceneTransition>().enterable == true){
			Destroy(this.gameObject);
		}
    }
}
