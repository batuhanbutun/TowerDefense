using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private List<GameObject> cannonTowerLevels;
    private bool canFire = true;

    [SerializeField] private GameObject cannonBallPrefab;
    [SerializeField] private Transform cannonBallShootingPos;
    
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
        cannonTowerLevels[towerLevel].SetActive(true);
        cannonTowerLevels[towerLevel - 1].SetActive(false);
    }
    
    public override void Attack()
    {
        if (detectedEnemies.Length > 0 && canFire)
        {
            target = closestEnemy;
            GameObject cannonBallGO = Instantiate(cannonBallPrefab, cannonBallShootingPos.position, cannonBallPrefab.transform.rotation);
            CannonBall cannonBall = cannonBallGO.GetComponent<CannonBall>();
            if(cannonBall != null)
                cannonBall.Seek(target);
            StartCoroutine(AttackDelay());
            canFire = false;
        }
    }
    
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.5f);
        canFire = true;
    }
}
