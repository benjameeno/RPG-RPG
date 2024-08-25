using System;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float rocketSpeed = 1f;
    
    private Rigidbody _rigidbody;

    private bool _rocketFired;
    
    public float explosionRadius = 1f;
    public float explosionPower = 1f;
    public float upwardsModifier = 1f;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void FireRocket()
    {
        if(_rocketFired) return;
        
        transform.SetParent(null);
        
        Vector3 moveDirection = transform.forward;
        
        _rigidbody.AddForce(moveDirection * rocketSpeed);

        _rocketFired = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(_rocketFired) RocketExplosion();
    }

    private void RocketExplosion()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionPower, explosionPos, explosionPower, upwardsModifier);
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
