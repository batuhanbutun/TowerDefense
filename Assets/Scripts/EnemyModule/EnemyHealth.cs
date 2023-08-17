using System;
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
    
    [SerializeField] private GameObject poisonedParticle;
    
    //Health Settings
    private float maxHealth = 100;
    private float currentHealth = 100;
    public bool isDead = false;
    private Shop shop;

    private int coinValue = 20;

    public string enemyType;
    private void Start()
    {
        shop = Shop.Instance;
        if (PlayerPrefs.GetInt(enemyType) != 3)
        {
            LockedEnemyCard.Instance.OpenCard(enemyType);
            PlayerPrefs.SetInt(enemyType,3);
        }
    }

    private void OnEnable()
    {
        currentHealth = 100;
        myMovementScript.canMove = true;
        healthBar.fillAmount = (currentHealth / maxHealth);
        myCollider.enabled = true;
        isDead = false;
    }

    public void GetDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.fillAmount = (currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        myAnimator.SetTrigger("die");
        myMovementScript.canMove = false;
        myCollider.enabled = false;
        isDead = true;
        poisonedParticle.SetActive(false);
        StartCoroutine(DeathDelay());
        ParticleManager.Instance.PlayBloodParticle(transform.position);
        shop.EarnCoin(coinValue);
    }

    public void PoisonDamage(int damageAmount)
    {
        poisonedParticle.SetActive(true);
        StartCoroutine(GetPoisonDamage(damageAmount));
    }

    IEnumerator GetPoisonDamage(int damageAmount)
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);
            GetDamage(damageAmount);
        }
    }
    
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
