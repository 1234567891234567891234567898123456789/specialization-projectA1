using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sfx,bgm;
    public AudioSource bgmSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        bgmSource.volume = sfxSource.volume = 10;
        PlayBGM("BackgroundMusic");
    }
    public void PlayBGM(string name)
    {
        Sound s = Array.Find(bgm, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            bgmSource.clip = s.clip;
            bgmSource.Play();
        }
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            //make each sound more varied
            sfxSource.PlayOneShot(s.clip);
        }
    }

}
