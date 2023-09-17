using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TurretDot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    
    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; // Bullets Per Second
    [SerializeField] private float DotDamege = 0.5f;

    private float apsBase;
    private float targetingRangeBase;
    private float timeUntilFire;


    private void Start(){
        apsBase = aps;
        targetingRangeBase = targetingRange;
    }

    private void Update(){
        timeUntilFire += Time.deltaTime;

        if(timeUntilFire >= 1f/aps){
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0){
            for(int i =0; i < hits.Length; i++){
                RaycastHit2D hit = hits[i];

                Health hl = hit.transform.GetComponent<Health>();
                hl.TakeDamage(DotDamege);
            }
        }
    }


    private void OnDrawGizmosSelected(){
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
