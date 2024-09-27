using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemySettings settings;

    protected bool isDead = false;

    public static event Action<Enemy> onEnemyDead;
    protected abstract void Move();
    protected abstract void Dead();    
    public abstract void TakeDamage(float damage);

    public void FixedUpdate()
    {
        Move();        
    }

    protected void DeadAction()
    { 
        onEnemyDead?.Invoke(this);
    }
}
