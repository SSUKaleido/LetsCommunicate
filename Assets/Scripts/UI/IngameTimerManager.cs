using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IngameTimerManager : MonoBehaviour
{
    public TMP_Text[] timeText;
    public Text gameOverText;
    public float time = 120; // 제한 시간 120초
    int min, sec;

    void Start()
    {
        //제한 시간 02:00
        timeText[0].text = "02";
        timeText[1].text = "00";

    }

    void Update()
    {
        if (time < 0)
        {
            IngameTimerDone();
        } 
        time -= Time.deltaTime;
        //timeValue = 0;

        min = (int)time / 60;
        sec = ((int)time - min * 60) % 60;

        if (min <= 0 && sec <= 0)
        {
            timeText[0].text = 0.ToString();
            timeText[1].text = 0.ToString();
        }

        else
        {
            if (sec >= 60)
            {
                min += 1;
                sec -= 60;
            }
            else
            {
                timeText[0].text = min.ToString();
                timeText[1].text = sec.ToString();
            }
        }
    }

    public void IngameTimerDone()
    {
        SceneManager.LoadScene("Scenes/TowerDefenseNight");
    }
}
