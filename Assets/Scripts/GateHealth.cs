using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GateHealth : MonoBehaviour
{
    
    public static GateHealth Instance;
    
    private float towerHealth = 100;
    private float currentTowerHealth;
    public Image healthFillableImage;

    private bool isGameOver = false;
    public GameObject gameOverPanel;
    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        currentTowerHealth = towerHealth;
    }
    
    public void GetDamage(int damageAmount)
    {
        currentTowerHealth -= damageAmount;
        float convertedHealth = currentTowerHealth / towerHealth;
        DOTween.To( () => healthFillableImage.fillAmount, ( newValue ) => healthFillableImage.fillAmount = newValue, convertedHealth,0.5f).OnComplete(() => HealthControl());
    }

    private void HealthControl()
    {
        if (currentTowerHealth <= 0 && !isGameOver)
        {
            gameOverPanel.SetActive(true);
            isGameOver = true;
        }
    }
}
