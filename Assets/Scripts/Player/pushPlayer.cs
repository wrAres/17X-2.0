using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushPlayer : MonoBehaviour
{
    private GameObject ItemtoPush = null;

    private bool avaliblePush = false;
    private bool currentlyPush = false;
    private Transform tempTransform;

    public PushHud Hud;

    private string[] pushItems;

    // Start is called before the first frame update
    void Start()
    {
        pushItems = new string[] { "Rock1", "Rock2", "Rock3", "Rock4", "Rock5", "Rock6", "Rock7", "Rock8", "Rock9", "Rock10"};
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemtoPush != null && Input.GetKeyDown(KeyCode.E) && avaliblePush)
        {
            currentlyPush = true;
            ItemtoPush.transform.parent = transform;
            Debug.Log("Made " + ItemtoPush.name + " Child of me");
        }
        if (Input.GetKeyUp(KeyCode.E) && ItemtoPush != null)
        {
            currentlyPush = false;
            ItemtoPush.transform.parent = tempTransform;
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Collide with" + collisionInfo.collider.name);
        Debug.Log("cuurpush" + currentlyPush);
        if (!currentlyPush)
        {
            Debug.Log(collisionInfo.collider.name);
            //ItemtoPush = collisionInfo.collider.gameObject;
            ItemtoPush = collisionInfo.collider.gameObject.transform.parent.gameObject;
            tempTransform = ItemtoPush.transform.parent; 
            foreach (string item in pushItems)
            {
                if (ItemtoPush.name == item)
                {
                    avaliblePush = true;
                    Debug.Log("Push is: " + avaliblePush);
                }
            }
        }
        Hud.OpenMessagePanel(collisionInfo.collider.name);
        Debug.Log("ShowPanel");
        /*if(collision.gameObject.tag == "Player"){
			//collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			collision.gameObject.GetComponent<playerMovement>().pushBack();
		}*/

    }

    void OnCollisionExit(Collision collisionInfo)
    {
        Hud.CloseMessagePanel();
        avaliblePush = false;
        Debug.Log("Push is: " + avaliblePush);
        //ItemtoPush.transform.parent = tempTransform;
        ItemtoPush = null;

    }
}
