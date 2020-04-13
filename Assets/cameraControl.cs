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
    void Start()
    {
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
		
		
        if (Input.GetKeyDown("z")) {
			def.enabled = !def.enabled;
			zoomed.enabled = !zoomed.enabled;
		}
    }
}
