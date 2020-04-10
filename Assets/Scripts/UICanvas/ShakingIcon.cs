using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingIcon : MonoBehaviour
{
    private bool shaking = false;
    [SerializeField]
    private float shakeAmt;
    private Vector3 originalPos;
    // Update is called once per frame
    void Start() {
        shakeAmt = 0.5f;
        originalPos = transform.position;
    }
    void Update () {
        if (shaking) {
            // RectTransform item_transform = this.GetComponent<RectTransform>();
            // Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * shakeAmt) + originalPos;
            Vector3 newPos = Random.insideUnitSphere * (shakeAmt * 0.002f) + originalPos;
            
            newPos.z = transform.position.z;
            // print("newPos" + newPos);
            transform.position = newPos;
            shakeAmt -= 15;
        }
    }

    public void ShakeMe() {
        StartCoroutine("ShakeNow");
    }

    IEnumerator ShakeNow() {
        if (shaking == false) {
            shaking = true;
        }

        yield return new WaitForSeconds(1f);

        shaking = false;
        transform.position = originalPos;
    }
}