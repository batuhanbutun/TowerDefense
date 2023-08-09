using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private List<GameObject> soldierLevels;

    [SerializeField] private Animator myAnimator;
    [SerializeField] private BarracksTower myTower;
    
    private Transform soldierFightPosition;
    public Transform movementTarget;
    private Transform fightPosition;
    public float movementSpeed;

    public Transform enemyTarget;
    private EnemyHealth enemyHealth;

    public bool isLockEnemy = false;

    private bool canAttack = true;
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isLockEnemy)
        {
            if (movementTarget != null)
            {
                Vector3 movementDirection = movementTarget.position - transform.position;
                movementDirection.y = 0f;
                Quaternion lookRotation = Quaternion.LookRotation(movementDirection.normalized);
                transform.Translate(movementDirection.normalized * (movementSpeed * Time.deltaTime), Space.World );
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
                myAnimator.SetBool("walking",true);
                if (Vector3.Distance(transform.position, movementTarget.position) < 0.15f)
                {
                    movementTarget = null;
                    isLockEnemy = false;
                    myAnimator.SetTrigger("idle");
                    myAnimator.SetBool("walking",false);
                }
            }
        } 
        if (enemyTarget != null && isLockEnemy)
        {
            if (Vector3.Distance(transform.position, enemyTarget.position) <= 0.3f)
            {
                if (canAttack)
                {
                    canAttack = false;
                    Attack();
                    StartCoroutine(AttackDelay());
                }
            }
            else
            {
                Vector3 movementDirection = enemyTarget.position - transform.position;
                movementDirection.y = 0f;
                Quaternion lookRotation = Quaternion.LookRotation(movementDirection.normalized);
                transform.Translate(movementDirection.normalized * (movementSpeed * Time.deltaTime), Space.World );
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
                myAnimator.SetBool("walking",true);
            }
        }
        else if (isLockEnemy)
        {
            movementTarget = soldierFightPosition;
            isLockEnemy = false;
            myTower.AddSoldierToReady(this);
        }
    }
    
    public void Spawn(Transform spawnPos, Transform fightPosition)
    {
        transform.position = spawnPos.position;
        movementTarget = fightPosition;
        soldierFightPosition = fightPosition;
        myTower.AddSoldierToReady(this);
    }
    
    public void FindNearbyEnemy(EnemyMovement enemy)
    {
        enemyTarget = enemy.transform;
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemy.SetTarget(this);
        isLockEnemy = true;
        myTower.DeleteSoldierToReady(this);
    }

    private void Attack()
    {
        myAnimator.SetTrigger("attack");
        enemyHealth.GetDamage(10);
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }

    public void SoldierLevelUp(int towerLevel)
    {
        soldierLevels[towerLevel].SetActive(true);
        soldierLevels[towerLevel - 1].SetActive(false);
        myAnimator = soldierLevels[towerLevel].GetComponent<Animator>();
    }
}
