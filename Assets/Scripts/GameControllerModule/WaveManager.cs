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
        public int spiderCount;
        public int magmaCount;
        public int golemCount;
        public int beeCount;
        public int wolfCount;
        public int totalUnitCountThisWave;
    }

    public List<WaveSettings> waves;
    [SerializeField] private Transform spawnPos;
    private float nextWaveTime = 10f;
    private int waveNumber = 0;
    private int enemyCountCurrentWave = 0;
    private PoolingManager poolingManager;

    private int currentDifficulty = 1;
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
        poolingManager = PoolingManager.Instance;
        StartCoroutine(SpawnEnemy());
    }
    

    IEnumerator SpawnEnemy()
    {
        while (enemyCountCurrentWave < waves[waveNumber].totalUnitCountThisWave)
        {
            yield return new WaitForSeconds(2f);
            poolingManager.SpawnFromPool(Enum.GetName(typeof(enemyTypes),Random.Range(0,currentDifficulty)),spawnPos.position,spawnPos.rotation);
            enemyCountCurrentWave++;
            if (enemyCountCurrentWave == waves[waveNumber].totalUnitCountThisWave)
            {
                StartCoroutine(WaveComplete());
                Debug.Log("dalga bitti");
            }
        }
    }

    IEnumerator WaveComplete()
    {
        yield return new WaitForSeconds(nextWaveTime);
        enemyCountCurrentWave = 0;
        currentDifficulty++;
        waveNumber++;
        StartCoroutine(SpawnEnemy());
        Debug.Log("yeni dalga basladi");
    }
    
}
