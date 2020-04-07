using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundScript : MonoBehaviour
{
    public static AudioClip backpackSound;
    public static AudioClip spellTreeIconSound;
    public static AudioClip wrongSpellSound;
    public static AudioClip openTalismanSound;
    public static AudioClip openSpellTreeSound;
    static AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        backpackSound = Resources.Load<AudioClip>("Sound/UI/get_new_spell");
        spellTreeIconSound = Resources.Load<AudioClip>("Sound/UI/get_new_element");
        wrongSpellSound = Resources.Load<AudioClip>("Sound/UI/wrong_spell");
        openTalismanSound = Resources.Load<AudioClip>("Sound/UI/open_talisman");
        openSpellTreeSound = Resources.Load<AudioClip>("Sound/UI/open_spell_tree");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = backpackSound;
        audioSources[1].clip = spellTreeIconSound;
        audioSources[2].clip = wrongSpellSound;
        audioSources[3].clip = openTalismanSound;
        audioSources[4].clip = openSpellTreeSound;
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
    public static void PlayWrongSpell()
    {
        audioSources[2].PlayOneShot(wrongSpellSound, 0.3f);
    }
    public static void OpenTalisman()
    {
        audioSources[3].PlayOneShot(openTalismanSound, 0.3f);
    }
    public static void OpenSpellTree()
    {
        audioSources[4].PlayOneShot(openSpellTreeSound, 0.3f);
    }
}
