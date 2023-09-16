using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameSceneManager : MonoBehaviour
{
    public void NewGameSkipStory()
    {
        SceneManager.LoadScene("Scenes/NewGameSelectTutorial");
    }
    public void NewGameTutorialYes()
    {
        SceneManager.LoadScene("Scenes/ChOne/Stage0");
    }
    public void NewGameTutorialNo()
    {
        SceneManager.LoadScene("Scenes/ChOne/Stage1");
    }
}
