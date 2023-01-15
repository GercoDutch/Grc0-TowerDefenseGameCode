using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Text waveCountdownText;


    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float countdown = 2f;
    private int waveIndex = 0;
    private bool waveEnded = true;

    public List<Transform> spawnPoints;
    private int mostRecentSpawn;

    void Update()
    {
        if (!waveEnded)
            return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        if(waveCountdownText) 
            waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveEnded = false;

        Wave wave = waves[waveIndex];
        
        for (int i = 0; i < wave.bowCount; i++)
        {
            SpawnEnemy(wave.enemy[0]);
            yield return new WaitForSeconds(wave.rate);
        }

        for (int i = 0; i < wave.tankCount; i++)
        {
            SpawnEnemy(wave.enemy[1]);
            yield return new WaitForSeconds(wave.rate);
        }

        for (int i = 0; i < wave.bombCount; i++)
        {
            SpawnEnemy(wave.enemy[2]);
            yield return new WaitForSeconds(wave.rate);
        }
        for (int i = 0; i < wave.bossCount; i++)
        {
            SpawnEnemy(wave.enemy[3]);
            yield return new WaitForSeconds(wave.rate);
        }

        waveIndex++;
        waveEnded = true;
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject spawnedEnemy = Instantiate(enemy, spawnPoints[mostRecentSpawn].position, spawnPoints[mostRecentSpawn].rotation);
        
        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
        enemyScript.spawn = spawnPoints[mostRecentSpawn];
        enemyScript.myWaveIndex = waveIndex;

        if (mostRecentSpawn + 1 < spawnPoints.Count)
            mostRecentSpawn++;
        else
            mostRecentSpawn = 0;
    }
}