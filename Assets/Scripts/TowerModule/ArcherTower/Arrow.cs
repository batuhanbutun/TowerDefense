using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public EnemyHealth enemyHealth;
    public float arcHeight;

    private Vector3 startPosition;
    private float stepScale;
    private float progress;
    private int damage;

    void Update() {
        if (target != null)
        {
            if (enemyHealth.isDead)
            {
                Destroy(gameObject);
                return;
            }
            progress = Mathf.Min(progress + Time.deltaTime * stepScale, 1.0f);
            float parabola = 1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5f);
            Vector3 nextPos = Vector3.Lerp(startPosition, target.position, progress);
            nextPos.y += parabola * arcHeight;
            transform.LookAt(nextPos, transform.forward);
            transform.position = nextPos;
            if(progress == 1.0f)
                Arrived(target);
        }
    }

    private void Arrived(Transform target)
    {
        target.gameObject.GetComponent<EnemyHealth>().GetDamage(damage);
        progress = 0;
        arcHeight = 0.5f;
        ParticleManager.Instance.PlayArrowParticle(target.position + new Vector3(0f,0.35f,0f));
        gameObject.SetActive(false);
        PoolingManager.Instance.GoToPool("arrow",this.gameObject);
    }

    public void Seek(Transform enemyTarget,int arrowDamage)
    {
        target = enemyTarget;
        damage = arrowDamage;
        startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, target.position);
        stepScale = speed / distance;
        arcHeight *= distance;
        enemyHealth = target.GetComponent<EnemyHealth>();
    }
    
}
