using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioMixer audioMixer;
    public AudioSource bgSFX;
    public AudioClip[] bgList;
    public AudioHighPassFilter filter;

    void Init()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Awake()
    {
        Init();
    }

    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgList.Length; i++)
        {
            if(arg0.name == bgList[i].name)
            {
                BGMPlay(bgList[i]);
            }
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + " Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();
        
        Destroy(go, clip.length);
    }

    public void BGMPlay(AudioClip clip)
    {
        bgSFX.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgSFX.clip = clip;
        bgSFX.loop = true;
        bgSFX.volume = 0.2f; 
        bgSFX.Play();
    }

    public void BGMSoundVolume(float amount)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(amount) * 10);
    }

    public void SFXSoundVolume(float amount)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(amount) * 10);
    }

    public void Filtering(bool flag)
    {
        filter.enabled = flag;
    }
}
