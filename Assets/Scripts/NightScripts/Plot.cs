using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    private Color startColor;
    public Turret turret;
    private GameObject towerSlowObj;
    public TurretSlowmo turretSlow;
    
    private void Start(){
        startColor = sr.color;
    }

    private void OnMouseEnter(){
        sr.color = hoverColor;
    }

    private void OnMouseExit(){
        sr.color = startColor;
    }

    private void OnMouseDown(){
        if(UIManager.main.IsHoveringUI()) {
            return;
        }

        if (towerObj != null){
            turret.OpenUpgradeUI();
            return;
        }

        if (towerSlowObj != null){
            turretSlow.OpenUpgradeUI();
            return;
        }
       

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency){
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        if(towerToBuild.cost == 10){
            towerSlowObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            turretSlow = towerSlowObj.GetComponent<TurretSlowmo>();
        
        }
        else{
            towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            turret = towerObj.GetComponent<Turret>();
        }
        
        
        
    }

}
