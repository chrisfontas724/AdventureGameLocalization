using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultAmbience;
    private AudioSource track01, track02; 
    private bool isPlayingTrack01; 
    public static AudioManager instance;
    public GameObject soundControlButton;
    public Sprite audioOffSprite; 
    public Sprite audioOnSprite;
    public AudioMixer theMixer;
  
    /* void Start()
        {
        //// Volume sliders in options menu 
        /// Sycns with players preferences 
            if(PlayerPrefs.HasKey("MasterVol"))
            {
                theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            }
            if(PlayerPrefs.HasKey("MusicVol"))
            {
                theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            }
            if(PlayerPrefs.HasKey("SFXVol"))
            {
                theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            }
        } */

    
    private void Awake()
    {
        if (instance == null){
            instance = this;
        }

    }

    private void Start()
    {
        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();
        isPlayingTrack01 = true; 
        
        SwapTrack(defaultAmbience);

            //// Volume sliders in options menu 
    /// Sycns with players preferences 
        if(PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        }
        if(PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        }
        if(PlayerPrefs.HasKey("SFXVol"))
        {
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
        }
    }
 
    public void SwapTrack(AudioClip newClip)
    { 
        if(isPlayingTrack01)
         {        
             track02.clip = newClip; 
             track02.Play();
             track01.Stop();
         }
         else
         {   track01.clip = newClip;
             track01.Play();
             track02.Stop();
         }

        //StopAllCoroutines();
        //StartCoroutines(FadeTrack(newClip));
        //isPlayingTrack01 = !isPlayingTrack01;
     // need to fix if you run because there will be lag 
    }
  
  // return to OG song 
  public void ReturnToDefault()
  {
    SwapTrack(defaultAmbience);
  }


  private IEnumerator FadeTrack(AudioClip newClip)
  { 
    float timeToFade = 1.25f; 
    float timeElapsed = 0; 

     if(isPlayingTrack01)
        {        
            track02.clip = newClip; 
            track02.Play();

            while (timeElapsed < timeToFade){
                track02.volume = Mathf.Lerp(0, 1, timeElapsed/ timeToFade);
                track01.volume = Mathf.Lerp(1, 0, timeElapsed/ timeToFade);
                timeElapsed += Time.deltaTime; 
                yield return null; 
            }

            track01.Stop();
        }
        else
        {   track01.clip = newClip;
            track01.Play();

            while (timeElapsed < timeToFade){
                track01.volume = Mathf.Lerp(0, 1, timeElapsed/ timeToFade);
                track02.volume = Mathf.Lerp(1, 0, timeElapsed/ timeToFade);
                timeElapsed += Time.deltaTime; 
                yield return null; 
            }
            
            track02.Stop();
        }
  }


}
