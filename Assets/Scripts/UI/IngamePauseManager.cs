using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngamePauseManager : MonoBehaviour
{
    public GameObject PausePanel;

    private void Awake()
    {
        PausePanel.SetActive(false);
    }
    public void IngamePauseClicked()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void IngamePauseResume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void IngamePauseTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/TitleScene");
    }
}
