using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager audioManager;
    AudioClip tempClip;

    void OnEnable()
    {
        PlayerMain.PlaySFX += PlaySound;
        // UIManager.PlaySFX += PlaySound;
        PlayerAttack.PlaySFX += PlaySound;
        PlayerAge.PlaySFX += PlaySound;
        PlayerHealth.PlaySFX += PlaySound;
        EnemyMain.PlaySFX += PlaySound;
        UIHourGlass.PlaySFX += PlaySound;
    }

    void OnDisable()
    {
        PlayerMain.PlaySFX -= PlaySound;
        // UIManager.PlaySFX -= PlaySound;
        PlayerAttack.PlaySFX -= PlaySound;
        PlayerAge.PlaySFX -= PlaySound;
        PlayerHealth.PlaySFX -= PlaySound;
        EnemyMain.PlaySFX -= PlaySound;
        UIHourGlass.PlaySFX -= PlaySound;
    }

    void Awake()
    {
        if(audioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            audioManager = this;
        }
        else if (audioManager != this)
            Destroy(gameObject);

        foreach(Sound s in sounds)
        {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;

                s.source.loop = s.loop;
        }
    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void SwitchBGM(int tilemapIndex)
    {
        Sound s;
        float time;
        
        if(tilemapIndex == 0)
        {
            s = Array.Find(sounds, sound => sound.name == "ThemeB");
            time = s.source.time; 
            s = Array.Find(sounds, sound => sound.name == "ThemeA");
            StopSound("ThemeB");
        }
        else
        {
            s = Array.Find(sounds, sound => sound.name == "ThemeA");
            time = s.source.time; 
            s = Array.Find(sounds, sound => sound.name == "ThemeB");
            StopSound("ThemeA");
        }

        if(!s.source.isPlaying)
        {    
            s.source.time = time;
            s.source.Play();
        }
    }
}