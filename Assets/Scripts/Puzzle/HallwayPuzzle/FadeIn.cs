using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Call startFading Function to start the fadein

public class FadeIn : MonoBehaviour
{
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
    }

    IEnumerator FadeInFunction() {
        float fadeSpeed = 0.01f; //Change fadeSpeed to manipulate the fading speed
        for (float f = fadeSpeed; f <= 1; f += fadeSpeed)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void startFading() 
    {
        StartCoroutine("FadeInFunction");
    }

}
