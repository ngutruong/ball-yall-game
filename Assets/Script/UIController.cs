using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    [SerializeField]
    GameObject StartMenuContentUI;
    [SerializeField]
    GameObject MultiplayerContentUI;
    [SerializeField]
    GameObject AboutContentUI;

    public void Campaign()
    {
        SceneManager.LoadScene("Campaign", LoadSceneMode.Single);
    }
    public void Multiplayer()
    {
        StartMenuContentUI.SetActive(false);
        MultiplayerContentUI.SetActive(true);
    }
    public void MultiplayerGoBack()
    {
        MultiplayerContentUI.SetActive(false);
        StartMenuContentUI.SetActive(true);
    }
    public void About()
    {
        StartMenuContentUI.SetActive(false);
        AboutContentUI.SetActive(true);
    }
    public void AboutGoBack()
    {
        AboutContentUI.SetActive(false);
        StartMenuContentUI.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
