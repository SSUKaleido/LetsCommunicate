using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI LifeUI;
    [SerializeField] TextMeshProUGUI WaveUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu(){
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI(){
        currencyUI.text = LevelManager.main.currency.ToString();
        LifeUI.text = LevelManager.main.life.ToString();
        WaveUI.text = LevelManager.main.currentWave.ToString();
    }

}
