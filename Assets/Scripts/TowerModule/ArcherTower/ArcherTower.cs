using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private List<GameObject> archerTowerLevels;
    [SerializeField] private List<Archer> archers;

    private bool canFire = true;
    private int damage;
    private float fireRate;

    [SerializeField] private TowerSettings archerTowerSettings;
    private void OnEnable()
    {
        towerLevel = 0;
        TowerInit();
    }

    private void Update()
    {
        DetectEnemy();
        Attack();
    }

    public override void Sell()
    {
        archerTowerLevels[towerLevel].SetActive(false);
        archerTowerLevels[0].SetActive(true);
        archers.Clear();
        towerLevel = 0;
        canFire = true;
        gameObject.SetActive(false);
    }
    
    public override void LevelUp()
    {
        archers.Clear();
        towerLevel++;
        archerTowerLevels[towerLevel].SetActive(true);
        archerTowerLevels[towerLevel - 1].SetActive(false);
        towerLevelUpAnim.DORestart();
        towerUpgradeCost += toAddTowerUpgradeCost;
        TowerInit();
    }

    public override void Attack()
    {
        if (detectedEnemies.Length > 0 && canFire)
        {
            target = closestEnemy;
            foreach (var archer in archers)
                archer.Attack(target,damage);
            StartCoroutine(AttackDelay());
            canFire = false;
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    public void AddArcherToTower(Archer archer)
    {
        archers.Add(archer);
    }

    private void TowerInit()
    {
        damage = archerTowerSettings.damageByLevels[towerLevel];
        towerRange = archerTowerSettings.towerRangeByLevels[towerLevel];
        fireRate = archerTowerSettings.fireRateByLevels[towerLevel];
    }
}
