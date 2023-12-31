using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPerfab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bps = 1f; // Bullets Per Second
    [SerializeField] private float bulletDamage = 1;

    private float bpsBase;
    private float targetingRangeBase;
    private float bulletDamageBase;

    private Transform target;
    private float timeUntilFire;


    private void Start(){
        bpsBase = bps;
        targetingRangeBase = targetingRange;
        bulletDamageBase = bulletDamage;
    }

    private void Update(){
        if (target == null){
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if(!CheckTargetIsRange()){
            target = null;
        } else{

            timeUntilFire += Time.deltaTime;

            if(timeUntilFire >= 1f/bps){
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot(){
        GameObject bulletObj = Instantiate(bulletPerfab, firingPoint.position, Quaternion.identity);
        Bullet bulletScipt = bulletObj.GetComponent<Bullet>();
        bulletScipt.SetTarget(target);
        bulletScipt.RotateTowardsTarget();
        bulletScipt.SetbulletDamage(bulletDamage);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length >0){
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsRange(){
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y -transform.position.y, target.position.x - transform.position.x )*Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed*Time.deltaTime);
    }


    private void OnDrawGizmosSelected(){
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
