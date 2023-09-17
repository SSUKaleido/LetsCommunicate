using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform stratPoint;
    //public List<List<Transform>> path;
    public Transform[] path;

    public int currency;
    public int life;
    public int currentWave;
    public int endWave;

    private void Awake() {
        main = this;
    }

    private void Start(){
        currentWave = 1;
    }

    public void IncreaseWave(){
        currentWave++;
    }

    public void DecreaseLife(){
        life--;
    }

    public void IncreaseCurrency(int amount){
        currency += amount;
    }

    public bool SpendCurrency(int amount){
        if (amount <= currency){
            currency -= amount;
            return true;
        } else{

            return false;
        }
    }
}
