using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private Animator myAnimator;
    private Transform enemyTarget;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowShootingPos;

    [SerializeField] private ArcherTower myTower;

    private PoolingManager poolingManager;
    private int arrowDamage;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        poolingManager = PoolingManager.Instance;
    }

    private void OnEnable()
    {
        myTower.AddArcherToTower(this);
    }

    private void Update()
    {
        if(enemyTarget != null)
            transform.LookAt(enemyTarget.position);
    }

    public void Attack(Transform target,int damage)
    {
        if (target != null)
        {
            enemyTarget = target;
            arrowDamage = damage;
            myAnimator.SetTrigger("fire");
            StartCoroutine(ArrowDelay());
        }
    }

    IEnumerator ArrowDelay()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject arrowGO = poolingManager.SpawnFromPool("arrow", arrowShootingPos.position, transform.rotation);
        if (arrowGO != null)
        {
            Arrow arrow = arrowGO.GetComponent<Arrow>();
            if(arrow != null)
                arrow.Seek(enemyTarget,arrowDamage);
        }
    }
}
