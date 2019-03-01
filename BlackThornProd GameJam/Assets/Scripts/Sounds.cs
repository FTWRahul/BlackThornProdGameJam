using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sounds : MonoBehaviour
{ 
        private AudioSource audioSrc;

        private float musicVolume = 1f;   //Music Vol. variable

    public AudioMixer masterMixer;

        // Use this for initialization
        void Start()
        {
            audioSrc = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            audioSrc.volume = musicVolume;
        }

        // Method that is called by slider game object: This method takes vol value passed by slider and sets it as musicValue
        public void SetVolume(float vol)
        {
            musicVolume = vol;
        }

    public void SetSfxLv(float sfxLvl)
    {
        masterMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLv(float musicLvl)
    {
        masterMixer.SetFloat("musicVol", musicLvl);
    }
}
