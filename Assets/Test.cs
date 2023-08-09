using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Transform enemyTarget;
    public float arrowSpeed;
    void Update()
    {
        if (enemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 movementDir = enemyTarget.position - transform.position;
        float distanceThisFrame = arrowSpeed * Time.deltaTime;

        if (movementDir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(movementDir.normalized * distanceThisFrame,Space.World);
        transform.LookAt(enemyTarget);
    }

    private void HitTarget()
    {
        Debug.Log("Hitting Something");
        Destroy(gameObject);
    }

    public void Seek(Transform target)
    {
        enemyTarget = target;
    }
    
}
