using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> explosionParticles,cannonParticles,mageParticles,arrowParticles,bloodParticles,coinParticles;
    private int explosionParticleIndex = 0,explosionParticleCount;
    private int mageParticleIndex = 0, mageParticleCount;
    private int arrowParticleIndex = 0, arrowParticleCount;
    private int bloodParticleIndex = 0, bloodParticleCount;

    public static ParticleManager Instance;

    private bool canSpawnParticle = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        explosionParticleCount = explosionParticles.Count - 1;
        mageParticleCount = mageParticles.Count - 1;
        arrowParticleCount = arrowParticles.Count - 1;
        bloodParticleCount = bloodParticles.Count - 1;
    }

    public void PlayExplosionParticle(Vector3 explosionPosition)
    {
        PlayParticle(explosionParticles[explosionParticleIndex],explosionPosition);
        PlayParticle(cannonParticles[explosionParticleIndex],explosionPosition);
        explosionParticleIndex++;
        if (explosionParticleIndex > explosionParticleCount)
            explosionParticleIndex = 0;
        StartCoroutine(ParticleDelay());
    }

    public void PlayMageParticle(Vector3 mageParticlePosition)
    {
        if (canSpawnParticle)
        {
            canSpawnParticle = false;
            PlayParticle(mageParticles[mageParticleIndex], mageParticlePosition);
            mageParticleIndex++;
            if (mageParticleIndex > mageParticleCount)
                mageParticleIndex = 0;
            StartCoroutine(ParticleDelay());
        }
    }
    
    public void PlayArrowParticle(Vector3 arrowParticlePosition)
    {
        if (canSpawnParticle)
        {
            canSpawnParticle = false;
            PlayParticle(arrowParticles[arrowParticleIndex], arrowParticlePosition);
            arrowParticleIndex++;
            if (arrowParticleIndex > arrowParticleCount)
                arrowParticleIndex = 0;
            StartCoroutine(ParticleDelay());
        }
    }

    public void PlayBloodParticle(Vector3 bloodPosition)
    {
        PlayParticle(coinParticles[bloodParticleIndex], bloodPosition);
        bloodPosition.y -= 0.16f;
        PlayParticle(bloodParticles[bloodParticleIndex], bloodPosition);
        bloodParticleIndex++;
        if (bloodParticleIndex > bloodParticleCount)
            bloodParticleIndex = 0;
    }

    private void PlayParticle(ParticleSystem particleSystem,Vector3 spawnPos)
    {
        particleSystem.transform.position = spawnPos;
        particleSystem.Play();
    }

    IEnumerator ParticleDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canSpawnParticle = true;
    }
    
}
