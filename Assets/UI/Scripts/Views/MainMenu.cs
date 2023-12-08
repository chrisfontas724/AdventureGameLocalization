using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class contains all the functionality
// assigned to buttons in the main menu.
public class MainMenu : View
{
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _creditsButton;

    public string firstlevel;
    public GameObject sceneLoaderPrefab;

    public override void Initialize()
    {
        _optionsButton.onClick.AddListener(() => ViewManager.Instance.Show<OptionsMenu>());
        _creditsButton.onClick.AddListener(() => ViewManager.Instance.Show<Credits>());
    }

    public void StartGame()
    {
        GameObject sceneLoader = Instantiate(sceneLoaderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        sceneLoader.GetComponent<LoadingScript>().LoadScene(firstlevel);
    }
    

    public void ContinueGame()
    {
        // TODO
    }


    public void QuitGame()
    {
        Application.Quit(0);
    }
}
