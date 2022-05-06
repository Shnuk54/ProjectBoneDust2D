using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager singleton;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider effectsSlider;
    [SerializeField] Slider musicSlider;
    private void Awake()
    {
        if(!singleton){
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(gameObject);
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
            
        }

        if(PlayerPrefs.HasKey("MusicVolume")){
           musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
           ChangeMusicVolume();
        }
        else{
            PlayerPrefs.SetFloat("MusicVolume",0f);
            ChangeMusicVolume();
        }

        if(PlayerPrefs.HasKey("EffectsVolume")){
           effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
           ChangeEffectsVolume();
        }
        else{
            PlayerPrefs.SetFloat("EffectsVolume",0f);
            ChangeEffectsVolume();
        }
    }
    
    private void Start()
    {
        
    }
    public  void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void ChangeMusicVolume(){
         foreach (Sound s in sounds)
        {
            s.source.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume",musicSlider.value);
            PlayerPrefs.SetFloat("MusicVolume",musicSlider.value);
        }
    }
     public void ChangeEffectsVolume(){
         foreach (Sound s in sounds)
        {
            s.source.outputAudioMixerGroup.audioMixer.SetFloat("EffectsVolume",effectsSlider.value);
            PlayerPrefs.SetFloat("EffectsVolume",effectsSlider.value);
        }
    }
    public void StopAll(){
          foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
    
}
