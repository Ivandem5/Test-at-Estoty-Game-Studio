using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthSystem : MonoBehaviour, IHealthSystem
{      
    private int curentHP;
    public event Action<int, int> OnHPChanged;
    private Player player;

    private GameManager manager;
    [Inject]
    private void Construct(GameManager manager)
    {
        this.manager = manager;
    }

    public void Init(Player player)
    {
        this.player = player;
        ChageHP(player.maxHP); // for UI update
    }

    public void ChageHP(int value)
    { 
        curentHP += value;
        curentHP = Math.Clamp(curentHP,0, player.maxHP);

        OnHPChanged?.Invoke(curentHP, player.maxHP);

        if (curentHP <= 0)
        { 
            manager.Restart();
        }        
    }

    public int GetCurrentHealth()
    {
        return curentHP;
    }   
}
