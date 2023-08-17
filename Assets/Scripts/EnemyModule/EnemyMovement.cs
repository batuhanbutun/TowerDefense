using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public Animator myAnimator;
    private Transform movementTarget;
    private int waypointIndex = 0;
    public float movementSpeed = 1f;

    public bool isLockSoldier = false;
    private bool isAttacking = false;
    
    public bool canMove = true;

    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private GameObject stunParticle;
    
    private void OnEnable()
    {
        waypointIndex = 0;
        movementTarget = Waypoints.points[0];
    }
    
    private void Update()
    {
        if(canMove)
            Movement();
    }

    private void Movement()
    {
        if (!isLockSoldier)
        {
            Move();
            if (Vector3.Distance(transform.position, movementTarget.position) <= 0.1f)
            {
                GetNextWaypoint();
            }
        }
        else
        {
            if (!movementTarget.gameObject.activeInHierarchy)
            {
                isLockSoldier = false;
                ContinueToWaypoint();
            }
            else
            {
                if (!isAttacking)
                {
                    if (Vector3.Distance(transform.position, movementTarget.position) <= 0.3f)
                    {
                        enemyAttack.Attack(movementTarget.GetComponent<Soldier>());
                        isAttacking = true;
                    }
                    else
                    {
                        Move();
                        isAttacking = false;
                    }
                }
                else
                {
                    enemyAttack.Attack(movementTarget.GetComponent<Soldier>());
                }
            }
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            GateHealth.Instance.GetDamage(10);
            gameObject.SetActive(false);
            waypointIndex = 0;
            return; 
        }
        waypointIndex++;
        movementTarget = Waypoints.points[waypointIndex];
    }

    private void ContinueToWaypoint()
    {
        movementTarget = Waypoints.points[waypointIndex];
        myAnimator.SetTrigger("walk");
    }
    
    public void SetTarget(Soldier soldier)
    {
        movementTarget = soldier.transform;
        isLockSoldier = true;
    }

    private void Move()
    {
        Vector3 movementDirection = movementTarget.position - transform.position;
        movementDirection.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(movementDirection.normalized);
        transform.Translate(movementDirection.normalized * (movementSpeed * Time.deltaTime), Space.World );
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    public void GetStun(float stunDuration)
    {
        canMove = false;
        myAnimator.SetTrigger("idle");
        stunParticle.SetActive(true);
        StartCoroutine(StunDelay(stunDuration));
    }

    IEnumerator StunDelay(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration);
        canMove = true;
        stunParticle.SetActive(false);
        ContinueToWaypoint();
    }
    
}
