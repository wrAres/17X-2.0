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
            DontDestroyVariables.baseDisappearCount += 1;
            Debug.Log("baseDisappearCount " + DontDestroyVariables.baseDisappearCount);

            if (push.ItemtoPush != null)
            {
                push.ItemtoPush.transform.parent = push.tempTransform;
                int index = Array.IndexOf(push.pushItems, push.ItemtoPush.name);
                push.pushItems[index] = null;
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
