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

    private int soldierIndex = 0;
    
    [SerializeField] private TowerSettings barracksTowerSettings;
    private int soldierDamage;
    private float soldierFireRate;
    private void OnEnable()
    {
        towerLevel = 0;
        TowerInit();
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
        TowerInit();
        towerUpgradeCost += toAddTowerUpgradeCost;
    }
    
    public override void Sell()
    {
        barracksTowerLevels[towerLevel].SetActive(false);
        barracksTowerLevels[0].SetActive(true);
        towerLevel = 0;
        towerUpgradeCost = towerUpgradeCostDefault;
        gameObject.SetActive(false);
        soldierIndex = 0;
        foreach (var soldier in soldiers)
        {
            soldier.gameObject.SetActive(false);
        }
    }
    
    public override void Attack()
    {
        soldiers[soldierIndex].gameObject.SetActive(true);
        soldiers[soldierIndex].Spawn(soldierSwapningPos,soldierFightPositions[soldierIndex]);
        soldierIndex++;
    }

    private void SetTargetForSoldiers()
    {
        if (freeSoldiers.Count > 0)
        {
            if (detectedEnemies.Length > 0)
            {
                foreach (var enemy in detectedEnemies)
                {
                    if (!enemy.GetComponent<EnemyMovement>().isLockSoldier)
                    {
                        freeSoldiers[0].FindNearbyEnemy(enemy.GetComponent<EnemyMovement>());
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
    
    private void TowerInit()
    {
        soldierDamage = barracksTowerSettings.damageByLevels[towerLevel];
        towerRange = barracksTowerSettings.towerRangeByLevels[towerLevel];
        soldierFireRate = barracksTowerSettings.fireRateByLevels[towerLevel];
        foreach (var soldier in soldiers)
        {
            soldier.SoldierInit(soldierDamage,soldierFireRate);
        }
    }
    
    
}
