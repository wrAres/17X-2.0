using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectSounds : MonoBehaviour
{
    public static AudioClip dirtInRiverSound;
    public static AudioClip fireBurnSound;
    static AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        dirtInRiverSound = Resources.Load<AudioClip>("Sound/Spell/dirt_in_water");
        fireBurnSound = Resources.Load<AudioClip>("Sound/Spell/burn_fire");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = dirtInRiverSound;
        audioSources[1].clip = fireBurnSound;
    }

    public static void PlayDirt()
    {
        audioSources[0].PlayOneShot(dirtInRiverSound, 0.5f);
    }

    public static void PlayFire()
    {
        audioSources[1].PlayOneShot(fireBurnSound, 0.05f);
    }
}
