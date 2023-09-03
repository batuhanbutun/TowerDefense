using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [SerializeField] private List<GameObject> mageTowerLevels;
    private bool canFire = true;
    [SerializeField] private GameObject mageSpellPrefab;
    [SerializeField] private Transform mageSpellShootingPos;
    
    
    [SerializeField] private TowerSettings mageTowerSettings;
    private int mageDamage;
    private float fireRate;
    private void OnEnable()
    {
        towerLevel = 0;
        if(poolingManager == null)
            poolingManager = PoolingManager.Instance;
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
        mageTowerLevels[towerLevel].SetActive(true);
        mageTowerLevels[towerLevel - 1].SetActive(false);
        towerLevelUpAnim.DORestart();
        towerUpgradeCost += toAddTowerUpgradeCost;
        TowerInit();
    }
    
    public override void Sell()
    {
        mageTowerLevels[towerLevel].SetActive(false);
        mageTowerLevels[0].SetActive(true);
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
            GameObject mageSpellGO = poolingManager.SpawnFromPool("magespell", mageSpellShootingPos.position, transform.rotation);
            MageSpell mageSpell = mageSpellGO.GetComponent<MageSpell>();
            if(mageSpell != null)
                mageSpell.Seek(target,mageDamage);
            StartCoroutine(AttackDelay());
            canFire = false;
        }
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }
    
    private void TowerInit()
    {
        mageDamage = mageTowerSettings.damageByLevels[towerLevel];
        towerRange = mageTowerSettings.towerRangeByLevels[towerLevel];
        fireRate = mageTowerSettings.fireRateByLevels[towerLevel];
    }
}
