using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : MonoBehaviour
{
    private Transform enemyTarget;
    private EnemyHealth enemyHealth;
    public float spellSpeed;
    private int damage;
    
    private PoolingManager poolingManager;
    private ParticleManager particleManager;
    private void Start()
    {
        poolingManager = PoolingManager.Instance;
        particleManager = ParticleManager.Instance;
    }
    
    void Update()
    {
        if (enemyHealth.isDead)
        {
            PoolingManager.Instance.GoToPool("magespell",this.gameObject);
            gameObject.SetActive(false);
            return;
        }
        Vector3 movementDir = enemyTarget.position - transform.position;
        float distanceThisFrame = spellSpeed * Time.deltaTime;

        if (movementDir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(movementDir.normalized * distanceThisFrame,Space.World);
    }

    private void HitTarget()
    {
        enemyHealth.GetDamage(damage,true,false);
        particleManager.PlayMageParticle(enemyTarget.position + new Vector3(0f,0.5f,0f));
        poolingManager.GoToPool("magespell",this.gameObject);
        gameObject.SetActive(false);
    }

    public void Seek(Transform target, int mageDamage)
    {
        enemyTarget = target;
        enemyHealth = enemyTarget.GetComponent<EnemyHealth>();
        damage = mageDamage;
    }
}
