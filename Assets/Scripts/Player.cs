using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Zenject;
using Zenject.SpaceFighter;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerShootSystem shootSystem;
    [SerializeField] private HealthSystem healthSystem;

    public int maxHP;
    public float speed;
    public float reloadTime;
    public float detectionRadius;

    public bool canRotationTowardJoystick;
    public bool isFacingRigt = true;    

    private InterfaceManager interfaceManager;
    private ISoundManager soundManager;

    [Inject]
    private void Costruct(InterfaceManager interfaceManager, ISoundManager soundManager)
    { 
        this.interfaceManager = interfaceManager;
        this.soundManager = soundManager;
        interfaceManager.Initialize(healthSystem);
    }

    private void Awake()
    {
        movement.Init(this);
        shootSystem.Init(this);
        healthSystem.Init(this);
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isFacingRigt = !isFacingRigt;
    }

    public void TakeDamage(int damage)
    {
        healthSystem.ChageHP(-damage);
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollidable collide))
        {            
            collide.Collide(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
