﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rivertip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Main Character")
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            DontDestroyVariables.haveSeenRiverTip = true;
            if (!DontDestroyVariables.accidentallyOpenTalisman)
                TipsDialog.PrintDialog("Talisman 1");
        }
    }
}