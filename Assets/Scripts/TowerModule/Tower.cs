using System;
using System.Collections;
using System.Collections.Generic;
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
    public abstract void Attack();
    public abstract void LevelUp();

    public void DetectEnemy()
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
