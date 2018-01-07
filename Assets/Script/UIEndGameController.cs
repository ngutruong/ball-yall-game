using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEndGameController : MonoBehaviour {

	public void Restart()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
