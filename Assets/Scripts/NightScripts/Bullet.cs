using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform bulletRotationPoint;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;

    private float bulletDamage;

    
    private Transform target;

    public void SetTarget(Transform _target){
        target = _target;
    }

    public void SetbulletDamage(float _bulletDamage){
        bulletDamage = _bulletDamage;
    }
    
    public void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y -transform.position.y, target.position.x - transform.position.x )*Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        bulletRotationPoint.rotation = Quaternion.RotateTowards(bulletRotationPoint.rotation, targetRotation, 100f);
    }

    private void FixedUpdate(){
        if (!target) {
            Destroy(gameObject);
            return;
        }
        
        Vector2 direction = (target.position - transform.position).normalized;
        RotateTowardsTarget();
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other){
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }

}
