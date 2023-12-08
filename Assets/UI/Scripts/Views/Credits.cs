using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : View
{
    [SerializeField] private Button _closeButton;


    // Start is called before the first frame update
    public override void Initialize()
    {
        _closeButton.onClick.AddListener(() => ViewManager.Instance.ShowLast());
    }
}
