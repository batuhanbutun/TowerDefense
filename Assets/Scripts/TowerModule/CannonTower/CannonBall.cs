using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float arcHeight;

    private Vector3 startPosition,targetStartPos;
    private float stepScale;
    private float progress;
    
    private Collider[] enemiesNearExplosion;
    public float explosionRange;
    public LayerMask explosionLayer;
    public int cannonDamage;
    void Update() {
       
            progress = Mathf.Min(progress + Time.deltaTime * stepScale, 1.0f);
            float parabola = 1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5f);
            Vector3 nextPos = Vector3.Lerp(startPosition, targetStartPos, progress);
            nextPos.y += parabola * arcHeight;
            transform.LookAt(nextPos, transform.forward);
            transform.position = nextPos;
            if(progress == 1.0f)
                Arrived();
        
    }

    private void Arrived()
    {
        enemiesNearExplosion = Physics.OverlapSphere(transform.position, explosionRange,explosionLayer);
        foreach (var enemy in enemiesNearExplosion)
        {
            enemy.GetComponent<EnemyHealth>().GetDamage(cannonDamage);
        }
        Destroy(gameObject);
    }

    public void Seek(Transform enemyTarget)
    {
        target = enemyTarget;
        targetStartPos = target.position;
        startPosition = transform.position;

        float distance = Vector3.Distance(startPosition, targetStartPos);
        
        stepScale = speed / distance;
    }
}
