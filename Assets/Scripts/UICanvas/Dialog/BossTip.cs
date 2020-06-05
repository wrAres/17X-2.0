using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTip : MonoBehaviour
{
    void Start()
    {
        // gameObject.GetComponent<Renderer>().enabled = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Main Character")
        {
            print("collide boxx tip");
            
            if (!DontDestroyVariables.windExist) {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                // GameObject.Find("1_water-dark").GetComponent<WaterPool>().timerActivated = true;
                // GameObject.Find("6_water-white").GetComponent<WaterPool>().timerActivated = true;

                GameObject.Find("1_water-dark").GetComponent<WaterPool>().changeColor();
                GameObject.Find("6_water-white").GetComponent<WaterPool>().changeColor();
                
                GameObject waterBoss = GameObject.Find("QiangYu");
                waterBoss.GetComponent<waterBoss>().appear();

                TipsDialog.PrintDialog("Water Boss");
            }
        }
    }
}
