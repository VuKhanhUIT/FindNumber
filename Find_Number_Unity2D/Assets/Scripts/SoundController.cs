using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundController : MonoSingleton<SoundController>
{
    public AudioClip[] audio;

    void Awake()
    {
        audio = Resources.LoadAll<AudioClip>("Audio");
    }
}
