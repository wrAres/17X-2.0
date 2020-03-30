using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSprout : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DontDestroyVariables.growSprout) {
            GameObject flowerpot = GameObject.Find("Flowerpot");
            flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Flowerpot with dirt");
            
            GameObject sprout = new GameObject("Water Sprout");
            sprout.transform.position = GameObject.Find("Flowerpot").transform.position + new Vector3(0f, 1.0f, 0f);
            sprout.transform.localScale = new Vector3(0.03f, 0.04f, 0.04f);
            Vector3 temp = sprout.transform.rotation.eulerAngles;
            temp.x = 45f;
            sprout.transform.rotation = Quaternion.Euler(temp);

            SpriteRenderer image = sprout.AddComponent<SpriteRenderer>(); //Add the Image Component script
            image.sprite = Resources.Load<Sprite>("ChangeAsset/Flower Bud"); //Set the Sprite of the Image Component on the new GameObject
            
            BoxCollider sproutCollider = sprout.AddComponent<BoxCollider>();
            sproutCollider.size = new Vector3(10f, 10f, 10f);
        }
    }
}
