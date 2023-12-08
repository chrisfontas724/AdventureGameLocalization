using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : View
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private TextMeshProUGUI _description;

    public override void Initialize() 
    {
        _resumeButton.onClick.AddListener(() => ViewManager.Instance.ShowLast());
        _optionsButton.onClick.AddListener(() => ViewManager.Instance.Show<OptionsMenu>());
    }

    public void SetDescriptionText(string text)
    {
        _description.text = text;
    }
}
