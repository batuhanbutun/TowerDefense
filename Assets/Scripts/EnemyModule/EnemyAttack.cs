using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public Animator myAnimator;
    private bool canAttack = true;
    
    private static readonly int Attack1 = Animator.StringToHash("attack");
    
    public void Attack(Soldier soldier)
    {
        if (canAttack)
        {
            canAttack = false;
            myAnimator.SetTrigger(Attack1);
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
        canAttack = true;
    }
}
