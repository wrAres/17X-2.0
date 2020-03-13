using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsDialog : MonoBehaviour
{
    public GameObject dialog;

    private void OnCollisionEnter(Collision collision){
     if(collision.gameObject.tag == "Player"){
      dialog.SetActive(true);
     }
    }
    private void OnCollisionExit(Collision collision){
     if(collision.gameObject.tag == "Player"){
      dialog.SetActive(false);
     }
    }
}