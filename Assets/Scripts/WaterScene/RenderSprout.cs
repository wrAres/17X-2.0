using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSprout : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {     
        if (DontDestroyVariables.growState == 0) {

        } else {
            GameObject.Find("Water Seed").SetActive(false);
            GameObject flowerpot = GameObject.Find("Flowerpot");
            if (DontDestroyVariables.growState == 1) {
                flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with dirt");
            } else if (DontDestroyVariables.growState == 2) {
                flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with seed");
            } else if (DontDestroyVariables.growState == 3) {
                flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with bud");
            } else if (DontDestroyVariables.growState == 4) {
                flowerpot.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ChangeAsset/Flowerpot/Flowerpot with Flower and Sun");
            }
        }
    }

}
