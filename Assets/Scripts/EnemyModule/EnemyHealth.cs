using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public Animator myAnimator;
    
    [SerializeField] private Image healthBar;

    [SerializeField] private BoxCollider myCollider;
    
    [SerializeField] private EnemyMovement myMovementScript;
    
    //Health Settings
    private float maxHealth = 100;
    private float currentHealth = 100;
    public void GetDamage(int damageAmount)
    {
        Debug.Log("DamageYedim");
        currentHealth -= damageAmount;
        healthBar.fillAmount = (currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Öldüm");
            myAnimator.SetTrigger("die");
            myMovementScript.canMove = false;
            myCollider.enabled = false;
    }
}
