using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ButtonTextScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI buttonText;

    void OnStart()
    {
        buttonText.color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
    }

    public void SetHighlight()
    {
        buttonText.color = new Color(1, 1, 1, 1);
    }

    public void FadeOut()
    {
        buttonText.color = new Color(0.4f, 0.4f, 0.4f, 1.0f);
    }

    void OnDisable()
    {
        FadeOut();
    }
}
