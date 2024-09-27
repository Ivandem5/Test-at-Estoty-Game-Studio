using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Zenject;

public class PlayerShootSystem : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform buletSpawnPoint;

    private GameManager manager;    
    private GameObject target;
    
    private float lastShootTime;

    private Player player;

    [HideInInspector] public bool canRotationTowardJoystick;

    private ISoundManager soundManager;

    [Inject]
    private void Costruct(InterfaceManager interfaceManager, ISoundManager soundManager)
    {        
        this.soundManager = soundManager;        
    }
    public void Init(Player player)
    {
        this.player = player;
    }
    private void Update()
    {
        CheckClosestEnemy();
        RotateWeaponAndPlayerTowardsEnemy();
        Shoot();
    }

    private void FixedUpdate()
    {
        
    }
    private void CheckClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, player.detectionRadius, enemyLayer);

        if (hits.Length == 0)
        {
            target = null;
            return;
        }

        float shortestDistance = Vector2.Distance(transform.position, hits[0].transform.position);
        target = hits[0].gameObject;

        foreach (Collider2D hit in hits)
        {

            float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);


            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                target = hit.gameObject;
            }
        }
    }

    private void RotateWeaponAndPlayerTowardsEnemy()
    {
        if (target == null)
        {
            player.canRotationTowardJoystick = true;
            gun.transform.rotation = Quaternion.identity;
            return;
        }
        player.canRotationTowardJoystick = false;
        Vector2 direction = target.transform.position - transform.position;

        if (direction.magnitude < 0.1f)
        {
            return;
        }    

        if ((target.transform.position.x < transform.position.x && player.isFacingRigt) || (target.transform.position.x > transform.position.x && !player.isFacingRigt)) //flip
        {
            player.Flip();            
        }

        float angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, player.isFacingRigt ? angle : 180 + angle));
        buletSpawnPoint.up = (target.transform.position - buletSpawnPoint.position).normalized;
    }

    

    private void Shoot()
    {
        if (target == null)
        {
            return;
        }

        if (Time.time >= lastShootTime + player.reloadTime)
        {            
            Bullet bullet = Instantiate(bulletPrefab, buletSpawnPoint.position, buletSpawnPoint.rotation); 
            lastShootTime = Time.time;
            soundManager.PlaySound("Fire");
        }
    }


    
}
