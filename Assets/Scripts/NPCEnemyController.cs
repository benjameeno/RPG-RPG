using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector2 moveDirection;
    
    public Transform playerCharacterTransform;

    public float moveSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (!playerCharacterTransform) Debug.LogError("Player Character transform not assigned");
    }

    private void Update()
    {
        transform.LookAt(playerCharacterTransform);
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward;
        
        _rigidbody.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}
