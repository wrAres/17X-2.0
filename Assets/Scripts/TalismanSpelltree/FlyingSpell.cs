using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingSpell : MonoBehaviour {

    public float iconSpeed;
    public GameObject spellIcon;
    public Transform spellTreeIcon, backpackIcon;

    bool isSpell, isBackpack;
    Vector3 origin;

    // Start is called before the first frame update
    void Start() {
        spellIcon.GetComponent<Image>().enabled = false;
        origin = spellIcon.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (isSpell || isBackpack) {
            Vector3 targetPos = spellTreeIcon.transform.position;
            if (isBackpack) targetPos = backpackIcon.transform.position;

            spellIcon.transform.position = Vector3.MoveTowards(spellIcon.transform.position, targetPos, iconSpeed * Time.deltaTime);
            if (Vector3.Distance(spellIcon.transform.position, targetPos) < 1) {
                if (isSpell)    { isSpell = false; spellTreeIcon.GetComponent<ShakingIcon>().ShakeMe(); }
                else            { isBackpack = false; backpackIcon.GetComponent<ShakingIcon>().ShakeMe(); }
                ResetFlyingSpell();
            }
        }
    }

    public void ResetFlyingSpell() {
        spellIcon.GetComponent<Image>().enabled = false;
        spellIcon.transform.position = origin;
        isSpell = false;
        isBackpack = false;
    }

    public void FlyTowardsIcon(Sprite s, bool isSpell) {
        this.isSpell = isSpell;
        isBackpack = !isSpell;
        spellIcon.GetComponent<Image>().enabled = true;
        spellIcon.GetComponent<Image>().sprite = s;
    }
}
