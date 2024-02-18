using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame()
    {

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif

    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameMap");
    }

    /*public void ContinueGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame 1");
    }*/
}
