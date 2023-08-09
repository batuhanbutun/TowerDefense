using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : MonoBehaviour
{
    private Transform enemyTarget;
    public float spellSpeed;
    void Update()
    {
        if (enemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 movementDir = enemyTarget.position - transform.position;
        float distanceThisFrame = spellSpeed * Time.deltaTime;

        if (movementDir.magnitude <= distanceThisFrame)
        {
            HitTarget(enemyTarget);
            return;
        }
        transform.Translate(movementDir.normalized * distanceThisFrame,Space.World);
    }

    private void HitTarget(Transform target)
    {
        Debug.Log("Hitting Mage");
        target.gameObject.GetComponent<EnemyHealth>().GetDamage(50);
        Destroy(gameObject);
    }

    public void Seek(Transform target)
    {
        enemyTarget = target;
    }
}
