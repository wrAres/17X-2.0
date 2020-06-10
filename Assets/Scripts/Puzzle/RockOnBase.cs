using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOnBase : MonoBehaviour
{

    private pushPlayer push;
    private bool currPush;

    private bool rockBack;
    private bool crystalBack;

    // Start is called before the first frame update
    void Start()
    {
        push = GameObject.Find("Main Character").GetComponent<pushPlayer>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        //print("collide some obj " + collision.gameObject + ", " + collision.gameObject.tag);
        //print("base " + this.gameObject.name);
        currPush = push.currentlyPush;
        //Debug.Log("Currpush in Base: " + currPush);
        if (currPush && push.ItemtoPush != null)
        {
            //Debug.Log("Base Collided with: " + push.ItemtoPush);
            //Debug.Log("Collider: " + collision.name);
            rockBack = push.ItemtoPush.tag.CompareTo("Rock") == 0 && this.gameObject.tag.CompareTo("RockBase") == 0;
            crystalBack = push.ItemtoPush.gameObject.tag.CompareTo("Crystal") == 0 && this.gameObject.tag.CompareTo("CrystalBase") == 0;
        }

        //print("rockBack: " + rockBack + ", CrystalBack: " + crystalBack);
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (crystalBack || rockBack)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("baseDisappearCount " + DontDestroyVariables.baseDisappearCount);

            if (push.ItemtoPush != null)
            {
                DontDestroyVariables.baseDisappearCount += 1;
                push.ItemtoPush.transform.parent = push.tempTransform;
                int index = Array.IndexOf(push.pushItems, push.ItemtoPush.name);
                push.pushItems[index] = null;

                // Move rock/crystal to base position
                Vector3 newpos = new Vector3(this.transform.position.x, push.ItemtoPush.transform.position.y, this.transform.position.z);
                push.ItemtoPush.transform.position = newpos;

                Vector3 distance = GameObject.Find("Main Character").transform.position - newpos;
                if (distance.magnitude <= 0.5) {
                    GameObject.Find("Main Character").transform.position = newpos - new Vector3(0, 0, 0.5f);
                }
                
                push.ItemtoPush = null;

                push.closeHUD();
                push.avaliblePush = false;
                push.currentlyPush = false;

                if (DontDestroyVariables.baseDisappearCount == 14) {
                    GameObject futureRockBase = GameObject.Find("FutureRock");
                    futureRockBase.GetComponent<SpriteRenderer>().enabled = true;
                    futureRockBase.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }
}
