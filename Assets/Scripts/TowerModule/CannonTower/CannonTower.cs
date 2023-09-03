using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private List<GameObject> cannonTowerLevels;
    private bool canFire = true;

    [SerializeField] private Transform cannonBallShootingPos;
    
    [SerializeField] private TowerSettings cannonTowerSettings;
    private int cannonDamage;
    private float fireRate;
    
    [SerializeField] private ParticleSystem cannonBallShootingParticle;
    private void OnEnable()
    {
        if(poolingManager == null)
            poolingManager = PoolingManager.Instance;
        towerLevel = 0;
        TowerInit();
    }
    
    private void Update()
    {
        DetectEnemy();
        Attack();
    }
    
    public override void LevelUp()
    {
        towerLevel++;
        cannonTowerLevels[towerLevel].SetActive(true);
        cannonTowerLevels[towerLevel - 1].SetActive(false);
        towerLevelUpAnim.DORestart();
        towerUpgradeCost += toAddTowerUpgradeCost;
        TowerInit();
    }
    
    public override void Sell()
    {
        cannonTowerLevels[towerLevel].SetActive(false);
        cannonTowerLevels[0].SetActive(true);
        towerLevel = 0;
        towerUpgradeCost = towerUpgradeCostDefault;
        gameObject.SetActive(false);
        canFire = true;
    }
    
    public override void Attack()
    {
        if (detectedEnemies.Length > 0 && canFire)
        {
            target = closestEnemy;
            GameObject cannonBallGO = poolingManager.SpawnFromPool("cannonball", cannonBallShootingPos.position, transform.rotation);
            CannonBall cannonBall = cannonBallGO.GetComponent<CannonBall>();
            if(cannonBall != null)
                cannonBall.Seek(target,cannonDamage);
            StartCoroutine(AttackDelay());
            canFire = false;
            cannonBallShootingParticle.Play();
        }
    }
    
    
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
    
    private void TowerInit()
    {
        cannonDamage = cannonTowerSettings.damageByLevels[towerLevel];
        towerRange = cannonTowerSettings.towerRangeByLevels[towerLevel];
        fireRate = cannonTowerSettings.fireRateByLevels[towerLevel];
    }
}
