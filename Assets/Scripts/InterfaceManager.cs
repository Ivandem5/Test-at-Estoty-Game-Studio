using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using System.ComponentModel.Design;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider expSlider;
    [SerializeField] private Text killsCounter;

    public float transtionSliderDuration;
    
    private int killsCount;
    private IHealthSystem healthSystem;

    public void Initialize(IHealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHPChanged += ChangeHP;
    }

    private void Start()
    {
        Enemy.onEnemyDead += Enemy_onEnemyDead;         
    }

    private void Enemy_onEnemyDead(Enemy obj)
    {
        killsCount++;
        killsCounter.text = killsCount.ToString();
    }

    private void ChangeHP(int arg1, int arg2)
    {
        healthSlider.DOValue((float)arg1 / (float)arg2, transtionSliderDuration);
    }

    private void OnDestroy()
    {
        healthSystem.OnHPChanged -= ChangeHP;
        Enemy.onEnemyDead -= Enemy_onEnemyDead;
    }


}
