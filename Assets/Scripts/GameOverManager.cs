using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Jackson Nevins
 * GameOverManager.cs
 * Controls the GameOver scene and allows the restart button to work
 */

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Easy");
    }

}
