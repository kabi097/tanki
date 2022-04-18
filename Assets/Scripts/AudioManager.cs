using UnityEngine.Audio;
using System;
using UnityEngine;

//Singleton AudioManager
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //Stores every Audio clips

    public static AudioManager instance; //Singleton instance

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds) //Loads every sounds with their settings
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    public void Play(string name) //Public function that plays the audio clip
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found.");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name) //Public function that stops the audio clip
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found.");
            return;
        }
        s.source.Stop();
    }

    public bool IsPlaying(string name) //Public function that returns true if audio clip is being played
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found.");
            return false;
        }
        return s.source.isPlaying;
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }


}
