using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushPlayer : MonoBehaviour
{
    public GameObject ItemtoPush = null;

    public bool avaliblePush = false;
    public bool currentlyPush = false;
    public Transform tempTransform;

    public PushHud Hud;

    public string[] pushItems;

    // Start is called before the first frame update
    void Start()
    {
        pushItems = new string[] { "Rock1", "Rock2", "Rock3", "Rock4", "Rock5", "Rock6", "Rock7", "Rock8", "Rock9", "Rock10",
                                   "Metal1", "Metal2", "Metal3", "Metal4", "Metal5"};
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CurrentlyPush: " + currentlyPush);
        if (ItemtoPush != null && Input.GetKeyDown(KeyCode.F) && avaliblePush)
        {
            currentlyPush = true;
            ItemtoPush.transform.parent = transform;
            //Debug.Log("Made " + ItemtoPush.name + " Child of me");
        }
        if (Input.GetKeyUp(KeyCode.F) && ItemtoPush != null)
        {
            currentlyPush = false;
            ItemtoPush.transform.parent = tempTransform;
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        // Debug.Log("Collide with" + collisionInfo.collider.name);
        // Debug.Log("cuurpush before " + currentlyPush);
        if (!currentlyPush)
        {
            //Debug.Log(collisionInfo.collider.name);
            //ItemtoPush = collisionInfo.collider.gameObject;
            foreach (string item in pushItems)
            {
                if (collisionInfo.collider.name.CompareTo(item) == 0)
                {
                    // Debug.Log(collisionInfo.collider.name + " true ");
                    avaliblePush = true;
                    ItemtoPush = collisionInfo.collider.gameObject.transform.parent.gameObject;
                    tempTransform = ItemtoPush.transform.parent; 
                    //Debug.Log("Push is: " + avaliblePush);
                    Hud.OpenMessagePanel(collisionInfo.collider.name);
                    //Debug.Log("ShowPanel");
                    break;
                }
            }
        }
        /*if(collision.gameObject.tag == "Player"){
			//collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
			collision.gameObject.GetComponent<playerMovement>().pushBack();
		}*/

    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (!currentlyPush)
        {
            closeHUD();
            avaliblePush = false;
            ItemtoPush = null;
        }
    }

    public void closeHUD()
    {
        Hud.CloseMessagePanel();
    }

    public void printItems()
    {
        string toPrint = "";
        foreach (string item in pushItems)
        {
            toPrint += item + ", ";
        }
        Debug.Log(toPrint);
    }
}
