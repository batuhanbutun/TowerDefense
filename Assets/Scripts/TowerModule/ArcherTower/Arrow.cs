using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float arcHeight;

    private Vector3 startPosition;
    private float stepScale;
    private float progress;
    

    void Update() {
        if (target != null)
        {
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
        Debug.Log("hit arrow");
        target.gameObject.GetComponent<EnemyHealth>().GetDamage(50);
        Destroy(gameObject);
    }

    public void Seek(Transform enemyTarget)
    {
        target = enemyTarget;
        startPosition = transform.position;

        float distance = Vector3.Distance(startPosition, target.position);
        
        stepScale = speed / distance;
    }
    
}
