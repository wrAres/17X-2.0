using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DontDestroyVariables.fromGame) {
            GameObject.Find("Finish 1").GetComponent<Image>().enabled = false;
            GameObject.Find("Finish 2").GetComponent<Image>().enabled = false;
            GameObject.Find("Prologue").GetComponent<Image>().enabled = false;
            GameObject.Find("Ends").GetComponent<Image>().enabled = false;

            this.gameObject.GetComponent<AudioSource>().volume = 0.8f;
        }
    }
}
