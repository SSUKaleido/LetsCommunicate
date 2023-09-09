using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
