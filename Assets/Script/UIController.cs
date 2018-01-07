using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    GameObject StartMenuContentUI;
    [SerializeField]
    GameObject MultiplayerContentUI;
    [SerializeField]
    GameObject AboutContentUI;
    [SerializeField]
    GameObject PlayerInfo;
    [SerializeField]
    GameObject GameControlsUI;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    Text TextLable;

    [SerializeField]
    InputField usernameInput;

    private string PLauerUsername;

    public void Campaign()
    {
        StartMenuContentUI.SetActive(false);
        GameControlsUI.SetActive(true);
    }
    public void GoBAckFromCampaign()
    {
        StartMenuContentUI.SetActive(true);
        GameControlsUI.SetActive(false);
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
    public void PlayerInfoPage()
    {
        StartMenuContentUI.SetActive(false);
        PlayerInfo.SetActive(true);
    }
    public void GoBackPlayerInfoPage()
    {
        PlayerPrefs.SetString("playerName", this.PLauerUsername);
        PlayerPrefs.SetString("color", Player.gameObject.GetComponent<Renderer>().material.color.ToString());
        StartMenuContentUI.SetActive(true);
        PlayerInfo.SetActive(false);
        Debug.Log(Player.gameObject.GetComponent<Renderer>().material.color.ToString());
    }
    public void SetPlayerUsername()
    {
        this.PLauerUsername = usernameInput.text;
        //TextLable.text = usernameInput.text;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartEasyGamePlay()
    {
        SceneManager.LoadScene("Campaign", LoadSceneMode.Single);
    }
    public void StartMediumGamePlay()
    {
        SceneManager.LoadScene("Campaign", LoadSceneMode.Single);
    }
    public void StartHardGamePlay()
    {
        SceneManager.LoadScene("Campaign", LoadSceneMode.Single);
    }
}
