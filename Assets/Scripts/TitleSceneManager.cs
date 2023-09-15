using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public void CallNewGame()
    {
        SceneManager.LoadScene("Scenes/NewGameStory");
    }
    public void CallLoadGame()
    {
        SceneManager.LoadScene("Scenes/LoadGame");
    }
    public void CallSettings()
    {
        SceneManager.LoadScene("Scenes/Settings");
    }
}
