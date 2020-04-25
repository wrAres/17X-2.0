using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EarthSoundManager : MonoBehaviour
{
    public static AudioClip playerStepSound;
    public static AudioClip player1StepSound;
    static AudioSource[] audioSources;
    public static bool triggerStep = false;
    public static bool shortStep = true;

    // Start is called before the first frame update
    void Start()
    {
        playerStepSound = Resources.Load<AudioClip>("Sound/Steps/step_earth");
        player1StepSound = Resources.Load<AudioClip>("Sound/Steps/step_earth_1");
        audioSources = this.gameObject.GetComponents<AudioSource>();
        audioSources[0].clip = playerStepSound;
        audioSources[1].clip = player1StepSound;
        // InvokeRepeating("PlaySound",0.001f,0.3f);
    }

    void Update() {
        
    }

    public static void PlaySound()
    {
        if (shortStep) {
            audioSources[1].volume = Random.Range(0.05f, 0.1f);
            audioSources[1].PlayOneShot(player1StepSound);
            shortStep = false;
        }
        else if (!triggerStep) {
            audioSources[0].Play(0);
            triggerStep = true;
        }
    }

    public static void StopPlaySound()
    {
        if (triggerStep) {
            audioSources[0].Stop();
            triggerStep = false;
            shortStep = true;
        }
    }
}
