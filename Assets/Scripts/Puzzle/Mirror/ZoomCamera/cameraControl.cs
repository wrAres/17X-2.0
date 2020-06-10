using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    // Start is called before the first frame update
	public Camera def;
	public Camera zoomed;
	AudioListener CameraDefAudio;
	AudioListener CameraZoomedAudio;
	public GameObject player;
	public bool active;
    void Start()
    {
		active = false;
		
        def.enabled = true;
		zoomed.enabled = false;
		CameraDefAudio = def.GetComponent<AudioListener>();
		CameraZoomedAudio = zoomed.GetComponent<AudioListener>();
		CameraDefAudio.enabled = true;
		CameraZoomedAudio.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		
		
        if (Input.GetKeyDown("z")&&active) {
			zoomed.GetComponent<Rigidbody>().transform.position = new Vector3(-2.41f,2.73f,-7.87f);
			def.enabled = !def.enabled;
			zoomed.enabled = !zoomed.enabled;
			if(zoomed.enabled){
				player.GetComponent<playerMovement>().freeze = 0;
				//player.GetComponent<playerMovement>().canAct = false;
			}else{
				player.GetComponent<playerMovement>().freeze = 1;
				//player.GetComponent<playerMovement>().canAct = true;
			}
		}
    }
}
