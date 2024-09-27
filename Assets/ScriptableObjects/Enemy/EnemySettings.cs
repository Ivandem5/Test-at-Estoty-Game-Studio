using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy",fileName = "Enemy")]
public class EnemySettings : ScriptableObject
{
    public int maxHP;    
    public int damage;
    public float speed;
    public float attackReload;
    public float bodyDestructionTime;
}
