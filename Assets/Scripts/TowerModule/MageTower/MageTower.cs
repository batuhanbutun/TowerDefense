using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [SerializeField] private List<GameObject> mageTowerLevels;
    private bool canFire = true;
    [SerializeField] private GameObject mageSpellPrefab;
    [SerializeField] private Transform mageSpellShootingPos;
    
    private int interval = 10;
    private void Start()
    {
        towerLevel = 0;
    }
    
    private void Update()
    {
        if(Time.frameCount % interval == 0)
            DetectEnemy();
        Attack();
    }
    
    public override void LevelUp()
    {
        towerLevel++;
        mageTowerLevels[towerLevel].SetActive(true);
        mageTowerLevels[towerLevel - 1].SetActive(false);
    }
    
    public override void Attack()
    {
        if (detectedEnemies.Length > 0 && canFire)
        {
            target = closestEnemy;
            GameObject mageSpellGO = Instantiate(mageSpellPrefab, mageSpellShootingPos.position, mageSpellPrefab.transform.rotation);
            MageSpell mageSpell = mageSpellGO.GetComponent<MageSpell>();
            if(mageSpell != null)
                mageSpell.Seek(target);
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
