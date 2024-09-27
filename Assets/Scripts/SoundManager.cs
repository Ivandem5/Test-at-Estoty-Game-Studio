using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, ISoundManager
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    public List<AudioItem> audioClips = new List<AudioItem>();    

    public void PlaySound(string name)
    {
        AudioItem audio = audioClips.Find(obj => obj.audioName == name);
        if (audio != null)
        {
            soundSource.PlayOneShot(audio.clip);            
        }
    }

    public void PlayMusic(string name)
    {
        AudioItem audio = audioClips.Find(obj => obj.audioName == name);
        if (audio != null)
        {
            musicSource.clip=audio.clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }   
}

[System.Serializable]
public class AudioItem
{ 
    public string audioName; 
    public AudioClip clip;
}
