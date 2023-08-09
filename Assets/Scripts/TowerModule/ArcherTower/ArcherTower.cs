using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField] private List<GameObject> archerTowerLevels;
    [SerializeField] private List<Archer> archers;

    private bool canFire = true;
    private void Start()
    {
        towerLevel = 0;
    }

    private void Update()
    {
        DetectEnemy();
        Attack();
    }

    public override void LevelUp()
    {
        towerLevel++;
        archerTowerLevels[towerLevel].SetActive(true);
        archerTowerLevels[towerLevel - 1].SetActive(false);
        archers.Clear();
    }

    public override void Attack()
    {
        if (detectedEnemies.Length > 0 && canFire)
        {
            target = closestEnemy;
            foreach (var archer in archers)
                archer.Attack(target);
            StartCoroutine(AttackDelay());
            canFire = false;
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.5f);
        canFire = true;
    }

    public void AddArcherToTower(Archer archer)
    {
        archers.Add(archer);
    }
}
