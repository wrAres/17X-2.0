using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOnBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        print("collide some obj " + collision.gameObject + ", " + collision.gameObject.tag);
        print("base " + this.gameObject.name);
        bool crystalBack = collision.gameObject.tag.CompareTo("Crystal") == 0 && this.gameObject.tag.CompareTo("CrystalBase") == 0;
        bool rockBack = collision.gameObject.tag.CompareTo("Rock") == 0 && this.gameObject.tag.CompareTo("RockBase") == 0;
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (crystalBack || rockBack)
        {
            print("collide correct obj");
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            DontDestroyVariables.baseDisappearCount += 1;
        }
    }
}
