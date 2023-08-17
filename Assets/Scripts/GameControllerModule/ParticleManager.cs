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
        explosionParticles[explosionParticleIndex].transform.position = explosionPosition;
        cannonParticles[explosionParticleIndex].transform.position = explosionPosition;
        cannonParticles[explosionParticleIndex].Play();
        explosionParticles[explosionParticleIndex].Play();
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
            mageParticles[mageParticleIndex].transform.position = mageParticlePosition;
            mageParticles[mageParticleIndex].Play();
            mageParticleIndex++;
            if (mageParticleIndex > mageParticleCount)
                mageParticleIndex = 0;
            StartCoroutine(ParticleDelay());
        }
    }
    
    public void PlayArrowParticle(Vector3 mageParticlePosition)
    {
        if (canSpawnParticle)
        {
            canSpawnParticle = false;
            arrowParticles[arrowParticleIndex].transform.position = mageParticlePosition;
            arrowParticles[arrowParticleIndex].Play();
            arrowParticleIndex++;
            if (arrowParticleIndex > arrowParticleCount)
                arrowParticleIndex = 0;
            StartCoroutine(ParticleDelay());
        }
    }

    public void PlayBloodParticle(Vector3 bloodPosition)
    {
        coinParticles[bloodParticleIndex].transform.position = bloodPosition;
        bloodPosition.y -= 0.16f;
        bloodParticles[bloodParticleIndex].transform.position = bloodPosition;
        coinParticles[bloodParticleIndex].Play();
        bloodParticles[bloodParticleIndex].Play();
        bloodParticleIndex++;
        if (bloodParticleIndex > bloodParticleCount)
            bloodParticleIndex = 0;
    }

    IEnumerator ParticleDelay()
    {
        yield return new WaitForSeconds(1.5f);
        canSpawnParticle = true;
    }
    
}
