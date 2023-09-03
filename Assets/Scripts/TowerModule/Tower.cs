using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public int towerLevel;
    public int maxTowerLevel;
    public float towerRange;
    public Collider[] detectedEnemies;
    public LayerMask detectionLayer;
    public Transform target;

    public Transform closestEnemy;
    private float closestDistance;
    
    protected int interval = 10;

    public int towerUpgradeCost;
    protected int towerUpgradeCostDefault;
    public int toAddTowerUpgradeCost;
    
    [SerializeField] private DOTweenAnimation towerSelectedAnim;
    [SerializeField] protected DOTweenAnimation towerLevelUpAnim;
    
    protected PoolingManager poolingManager;
    public abstract void Attack();
    public abstract void LevelUp();
    public abstract void Sell();

    private void Start()
    {
        towerUpgradeCostDefault = towerUpgradeCost;
    }

    public void DetectEnemy()
    {
        if (Time.frameCount % interval == 0)
        {
            detectedEnemies = Physics.OverlapSphere(transform.position, towerRange,detectionLayer);
            closestDistance = 100f;
            foreach (var enemy in detectedEnemies)
            {
                var newDistance = (enemy.transform.position - transform.position).sqrMagnitude;
                if (newDistance < closestDistance)
                {
                    closestEnemy = enemy.transform;
                    closestDistance = newDistance;
                }
            }
        }
    }

    public void PlaySelectedAnimation()
    {
        towerSelectedAnim.DOPlay();
    }
    
}
