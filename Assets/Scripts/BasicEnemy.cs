using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using DG.Tweening;
using Zenject;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class BasicEnemy : Enemy, ICollidable
{    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform target;
    private float currentHP;
    private float lastAttackTime;

    public void SetTarget(Transform target)
    { 
        this.target = target;
        currentHP = settings.maxHP;
    }
    protected override void Move()
    {
        if (isDead)
        {
            rb.velocity=Vector2.zero;
            return;
        }
        Vector2 direction = target.position-transform.position;
        rb.velocity = direction.normalized*settings.speed;
        
        if (Vector2.Angle(transform.right, direction) > 90 && direction.magnitude>0.1f) //flip
        {
            transform.Rotate(new Vector3(0,180,0));
        }
    }

    public override void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        currentHP-=damage;
        if (currentHP <= 0)
        {
             Dead();
        }
        animator.SetTrigger("Hit");
    }
    protected override void Dead()
    {        
        animator.SetBool("Dead", true);
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        DeadAction();



        spriteRenderer.DOFade(0f, settings.bodyDestructionTime).OnComplete(() =>
        {            
            Destroy(gameObject);
        });
        
    }    

    public void Collide(Player player)
    {
        if (isDead) 
        {
            return;
        }
        if (Time.time >= lastAttackTime + settings.attackReload)
        {
            player.TakeDamage(settings.damage);
            lastAttackTime = Time.time;
        }        
    }
}
