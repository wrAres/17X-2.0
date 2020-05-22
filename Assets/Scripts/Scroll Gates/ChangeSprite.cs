using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite Target;
    public bool Trigger = false;
    // Start is called before the first frame update


    // Update is called once per frame
    public void OpenScroll() {
        Trigger = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Target;
        Vector3 size = this.gameObject.GetComponent<BoxCollider>().size;
        this.gameObject.GetComponent<BoxCollider>().size = new Vector3(17f, size.y, size.z);
        size = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider>().size;
        this.gameObject.transform.GetChild(0).GetComponent<BoxCollider>().size = new Vector3(17f, size.y, size.z);
        string name = this.gameObject.name;
        sceneTransition transition = this.gameObject.GetComponent<sceneTransition>();
        UISoundScript.OpenTalisman(); // Here for open scroll gate
        transition.CheckOpen();
    }
}
