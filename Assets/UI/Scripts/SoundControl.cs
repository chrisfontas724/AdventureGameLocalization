using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{   
    public GameObject soundControlButton;
    public Sprite audioOffSprite; 
    public Sprite audioOnSprite;

    void Start()
    {

    ///// Music on/off with pausing
    if (AudioListener.pause == true){
        soundControlButton.GetComponent<Image>().sprite = audioOffSprite;
    } else {
        soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
    }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void soundControlB(){
    if (AudioListener.pause== true){
        AudioListener.pause = false;
        soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
        
    } else{
        AudioListener.pause = true;
        soundControlButton.GetComponent<Image>().sprite = audioOffSprite;   
        }
    }
 }




