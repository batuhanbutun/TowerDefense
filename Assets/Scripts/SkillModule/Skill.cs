using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public LayerMask detectionLayer;
    public Collider[] detectedEnemies;
    public float skillRange;
    public bool isSkillReady;
    
    [SerializeField] protected ParticleSystem skillParticle;

    [SerializeField] protected float skillCooldown;
    [SerializeField] protected Image skillGrayImage;
    public float timeDuration = 0f;

    [SerializeField] protected DOTweenAnimation skillButtonAnim;
    
    public abstract void CastSkill(Vector3 position);

    public void DetectEnemy()
    {
        detectedEnemies = Physics.OverlapSphere(transform.position, skillRange,detectionLayer);
    }

    public void SkillCooldown()
    {
        if (timeDuration <= skillCooldown)
        {
            timeDuration += Time.deltaTime;
            skillGrayImage.fillAmount = 1f - (timeDuration / skillCooldown);
        }
        else if (!isSkillReady)
            isSkillReady = true;
    }

    public void SkillAnim()
    {
        skillButtonAnim.DORestart();
    }
}
