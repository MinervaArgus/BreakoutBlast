using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Jackson Nevins
 * MainMenu.cs
 * Controls the Scene for the MainMenu and handles its buttons
 */

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayerNameInput");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
