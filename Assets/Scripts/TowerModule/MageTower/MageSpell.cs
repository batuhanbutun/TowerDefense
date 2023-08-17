using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : MonoBehaviour
{
    private Transform enemyTarget;
    private EnemyHealth enemyHealth;
    public float spellSpeed;
    private int damage;
    void Update()
    {
        if (enemyHealth.isDead)
        {
            Destroy(gameObject);
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
        enemyHealth.GetDamage(damage);
        ParticleManager.Instance.PlayMageParticle(enemyTarget.position + new Vector3(0f,0.5f,0f));
        PoolingManager.Instance.GoToPool("magespell",this.gameObject);
        gameObject.SetActive(false);
    }

    public void Seek(Transform target, int mageDamage)
    {
        enemyTarget = target;
        enemyHealth = enemyTarget.GetComponent<EnemyHealth>();
        damage = mageDamage;
    }
}
