using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksTower : Tower
{
    [SerializeField] private List<GameObject> barracksTowerLevels;
    [SerializeField] private List<Soldier> soldiers;
     
    [SerializeField] private List<Transform> soldierFightPositions;
    [SerializeField] private Transform soldierSwapningPos;
    
    public List<Soldier> freeSoldiers;
    private bool canSpawnSoldier = true;
    private bool isSoldierFree = true;

    private int soldierIndex = 0;
    private int interval = 10;
    private void Start()
    {
        towerLevel = 0;
        Attack();
    }

    private void Update()
    {
        DetectEnemy();
        SetTargetForSoldiers();
    }

    public override void LevelUp()
    {
        towerLevel++;
        barracksTowerLevels[towerLevel].SetActive(true);
        barracksTowerLevels[towerLevel - 1].SetActive(false);
        Attack();
        foreach (var soldier in soldiers)
        {
            soldier.SoldierLevelUp(towerLevel);
        }
    }
    
    public override void Attack()
    {
        soldiers[soldierIndex].gameObject.SetActive(true);
        soldiers[soldierIndex].Spawn(soldierSwapningPos,soldierFightPositions[0]);
        soldierIndex++;
        soldierFightPositions.Remove(soldierFightPositions[0]);
    }

    private void SetTargetForSoldiers()
    {
        if (freeSoldiers.Count > 0)
        {
            if (detectedEnemies.Length > 0)
            {
                int enemyIndex = 0;
                foreach (var enemy in detectedEnemies)
                {
                    if (!enemy.GetComponent<EnemyMovement>().isLockSoldier)
                    {
                        freeSoldiers[0].FindNearbyEnemy(detectedEnemies[enemyIndex].GetComponent<EnemyMovement>());
                        break;
                    }
                }
            }
        }
    }

    public void AddSoldierToReady(Soldier soldier)
    {
        freeSoldiers.Add(soldier);
    }
    
    public void DeleteSoldierToReady(Soldier soldier)
    {
        freeSoldiers.Remove(soldier);
    }
    
}
