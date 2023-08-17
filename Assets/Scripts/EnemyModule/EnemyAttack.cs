using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public Animator myAnimator;
    private bool canAttack = true;

    public void Attack(Soldier soldier)
    {
        if (canAttack)
        {
            canAttack = false;
            myAnimator.SetTrigger("attack");
            Debug.Log("Attack from Enemy");
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }
}
