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
    public float maxHealth = 100;
    private float currentHealth;
    public bool isDead = false;
    private Shop shop;

    public int coinValue = 20;

    public string enemyType;

    public int magicResist;
    public int armor;

    public string tag;
    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        if (PlayerPrefs.GetInt(enemyType) != 3)
        {
            LockedEnemyCard.Instance.OpenCard(enemyType);
            PlayerPrefs.SetInt(enemyType,3);
        }
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        myMovementScript.canMove = true;
        healthBar.fillAmount = (currentHealth / maxHealth);
        myCollider.enabled = true;
        isDead = false;
    }

    public void GetDamage(int damageAmount,bool isMagicDamage,bool isTrueDamage)
    {
        if (isTrueDamage)
            currentHealth -= damageAmount;
        else
        { 
            if (isMagicDamage)
                damageAmount -= magicResist;
            else
                damageAmount -= armor;
            if (damageAmount <= 0)
                damageAmount = 10;
            currentHealth -= damageAmount;
        } 
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
        PoolingManager.Instance.GoToPool(tag,this.gameObject);
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
            GetDamage(damageAmount,false,true);
        }
    }
    
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
