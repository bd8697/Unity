using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWaveIdx = 0;
    [SerializeField] int loops = 1;

    int currLoop = 0;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves()); // #Fenci
        }
        while (currLoop < loops);
    }

    private IEnumerator SpawnAllWaves()
    {
       for(int i = startingWaveIdx; i < waveConfigs.Count; i++)
        {
            var currWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currWave));
        }
        currLoop++;
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        var count = waveConfig.GetEnemyCount();
        for(int i = 0; i < count; i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetwayPoints()[0].transform.position, Quaternion.identity); //Quaternion.identity = default rotation
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
