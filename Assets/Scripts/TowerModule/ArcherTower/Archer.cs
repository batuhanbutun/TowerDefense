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
    
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myTower.AddArcherToTower(this);
    }

    private void Update()
    {
        if(enemyTarget != null)
            transform.LookAt(enemyTarget.position);
    }

    public void Attack(Transform target)
    {
        if (target != null)
        {
            enemyTarget = target;
            myAnimator.SetTrigger("fire");
            StartCoroutine(ArrowDelay());
        }
    }

    IEnumerator ArrowDelay()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject arrowGO = Instantiate(arrowPrefab, arrowShootingPos.position, arrowPrefab.transform.rotation);
        Arrow arrow = arrowGO.GetComponent<Arrow>();
        if(arrow != null)
            arrow.Seek(enemyTarget);
    }
    
    
}
