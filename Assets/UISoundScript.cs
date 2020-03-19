using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundScript : MonoBehaviour
{
    public static AudioClip backpackSound;
    public static AudioClip spellTreeIconSound;
    static AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        backpackSound = Resources.Load<AudioClip>("Sound/UI/get_new_spell");
        spellTreeIconSound = Resources.Load<AudioClip>("Sound/UI/get_new_element");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = backpackSound;
        audioSources[1].clip = spellTreeIconSound;
        // InvokeRepeating("PlaySound",0.001f,0.3f);
    }

    public static void PlayBackpack()
    {
        audioSources[0].PlayOneShot(backpackSound, 0.05f);
    }

    public static void PlaySpellTreeIcon()
    {
        audioSources[1].PlayOneShot(spellTreeIconSound, 0.05f);
    }
}
