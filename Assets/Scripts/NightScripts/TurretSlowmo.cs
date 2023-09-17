using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TurretSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    
    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; // Bullets Per Second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private float freezeSpeed = 1f;

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

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(freezeSpeed);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em){
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }
    

    private void OnDrawGizmosSelected(){
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
