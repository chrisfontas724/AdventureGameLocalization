using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextModifier : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI buttonText;

    string[] text_options = new string[7]{ "Don't forget who you are...",
                                           "In trying times...turn to the light",
                                           "All is not as it appears to be...", 
                                           "Try to survive...", 
                                           "The darkness comes...",
                                           "Death comes for us all...",
                                           "It was not death, for I stood up"}; // Quote by Emily Dickenson


   // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, text_options.Length);
        buttonText.text = text_options[index];
    }

}
