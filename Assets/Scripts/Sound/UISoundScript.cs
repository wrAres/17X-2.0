
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
    public static AudioClip pickObjSound;
    public static AudioClip[] dialogNextSounds;
    static AudioSource[] audioSources;
    public static bool next = false;

    // Start is called before the first frame update
    void Start()
    {
        backpackSound = Resources.Load<AudioClip>("Sound/UI/get_new_spell");
        spellTreeIconSound = Resources.Load<AudioClip>("Sound/UI/get_new_element");
        wrongSpellSound = Resources.Load<AudioClip>("Sound/UI/wrong_spell");
        openTalismanSound = Resources.Load<AudioClip>("Sound/UI/open_talisman");
        openSpellTreeSound = Resources.Load<AudioClip>("Sound/UI/open_spell_tree");
        pickObjSound = Resources.Load<AudioClip>("Sound/UI/pick");

        dialogNextSounds = new AudioClip[2];
        dialogNextSounds[0] = Resources.Load<AudioClip>("Sound/UI/dialog_next");
        dialogNextSounds[1] = Resources.Load<AudioClip>("Sound/UI/dialog_next1");


        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = backpackSound;
        audioSources[1].clip = spellTreeIconSound;
        audioSources[2].clip = wrongSpellSound;
        audioSources[3].clip = openTalismanSound;
        audioSources[4].clip = openSpellTreeSound;
        audioSources[5].clip = dialogNextSounds[0];
        audioSources[6].clip = pickObjSound;
        // InvokeRepeating("PlaySound",0.001f,0.3f);
    }

    public static void PlayGetItem()
    {
        audioSources[0].PlayOneShot(backpackSound, 0.05f);
    }

    public static void PlayGetElement()
    {
        audioSources[1].PlayOneShot(spellTreeIconSound, 0.05f);
    }
    public static void PlayWrongSpell()
    {
        audioSources[2].Play();
    }
    public static void OpenTalisman()
    {
        audioSources[3].PlayOneShot(openTalismanSound, 0.1f);
    }
    public static void OpenSpellTree()
    {
        audioSources[4].PlayOneShot(openSpellTreeSound, 0.1f);
    }
    public static void PlayDialogNext()
    {
        int nextInt = next ? 1 : 0;
        audioSources[5].PlayOneShot(dialogNextSounds[nextInt], 0.5f);
        next = !next;
    }
    public static void PlayPick()
    {
        audioSources[6].Play();
    }
}