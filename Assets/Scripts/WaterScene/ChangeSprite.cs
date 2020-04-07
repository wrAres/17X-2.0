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
        string name = this.gameObject.name;
        sceneTransition transition = this.gameObject.GetComponent<sceneTransition>();
        transition.CheckOpen();
    }
}
