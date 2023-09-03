using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveSettings
    {
        public int spider;
        public int magma;
        public int golem;
        public int bee;
        public int wolf;
        public int totalUnitCountThisWave;
    }

    public List<WaveSettings> waves;
    [SerializeField] private Transform spawnPos;
    private float nextWaveTime = 10f;
    private int waveNumber = 0;
    private int enemyCountCurrentWave = 0;
    private PoolingManager poolingManager;

    private int currentDifficulty = 1;
    private int maxWaveCount;
    public enum enemyTypes
    {
        spider,
        wolf,
        magma,
        bee,
        golem
    }
    
    private void Start()
    {
        maxWaveCount = waves.Count;
        poolingManager = PoolingManager.Instance;
        StartCoroutine(SpawnEnemy());
    }
    

    IEnumerator SpawnEnemy()
    {
        while (enemyCountCurrentWave < waves[waveNumber].totalUnitCountThisWave)
        {
            yield return new WaitForSeconds(2f);
            poolingManager.SpawnFromPool(SpawnEnemyFromWave(),spawnPos.position,spawnPos.rotation);
            enemyCountCurrentWave++;
            if (enemyCountCurrentWave == waves[waveNumber].totalUnitCountThisWave)
            {
                StartCoroutine(WaveComplete());
            }
        }
    }

    IEnumerator WaveComplete()
    {
        yield return new WaitForSeconds(nextWaveTime);
        enemyCountCurrentWave = 0;
        currentDifficulty++;
        waveNumber++;
        if(waveNumber < maxWaveCount)
            StartCoroutine(SpawnEnemy());
    }

    public string SpawnEnemyFromWave()
    {
        int enemyNo = 0;
        if (waves[waveNumber].spider > 0)
        {
            enemyNo = 0;
            waves[waveNumber].spider--;
        }
        else if (waves[waveNumber].wolf > 0)
        {
            enemyNo = 1;
            waves[waveNumber].wolf--;
        }
        else if (waves[waveNumber].magma > 0)
        {
            enemyNo = 2;
            waves[waveNumber].magma--;
        }
        else if (waves[waveNumber].bee > 0)
        {
            enemyNo = 3;
            waves[waveNumber].bee--;
        }
        else if (waves[waveNumber].golem > 0)
        {
            waves[waveNumber].golem--;
            enemyNo = 4;
        }
        
        return Enum.GetName(typeof(enemyTypes), enemyNo);
    }
    
}
