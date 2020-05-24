using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<playerMovement>().checkpoint = this.transform.position;
		}
		Debug.Log(this.transform.position);
	}
}
