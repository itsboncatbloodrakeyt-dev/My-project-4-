using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioSource Music;
    public AudioSource SFX;

    public AudioClip musiquita;
    public AudioClip agarrarObjeto;

    public AudioClip NPCChat;

    public AudioClip cash;
    
    public AudioMixer Mixer;

    public AudioMixerGroup MusicGroup;
    public AudioMixerGroup SFXGroup;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Music.clip = musiquita;
        Music.outputAudioMixerGroup = MusicGroup;
        Music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Agarrar()
    {
        SFX.clip = agarrarObjeto;
        SFX.outputAudioMixerGroup = SFXGroup;
        SFX.Play();
    }
    public void Plata()
    {
        SFX.clip = cash;
        SFX.outputAudioMixerGroup = SFXGroup;
        SFX.Play();
    }
     public void NPCS()
    {
        SFX.clip = NPCChat;
        SFX.outputAudioMixerGroup = SFXGroup;
        SFX.Play();
    }
}
