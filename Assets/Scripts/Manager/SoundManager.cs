using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private List<AudioSource> audioSources;

    public void ChangeVoulme(SoundType soundType,float value)
    {
        int index = (int)soundType;
        audioSources[index].volume = value;
    }

    public void PlayOneShot(AudioClip clip)
    {
        int index = (int)SoundType.SFX;
        audioSources[index].PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip clip)
    {
        int index = (int)SoundType.BGM;

        audioSources[index].Stop();
        audioSources[index].clip = clip;
        audioSources[index].Play();
    }
}
