using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRndFactor = 0.5f;
    [SerializeField] int enemyCount = 10;
    [SerializeField] float speed = 3f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> GetwayPoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    public float GetSpawnRndFactor()
    {
        return spawnRndFactor;
    }
    public int GetEnemyCount()
    {
        return enemyCount;
    }
    public float GetSpeed()
    {
        return speed;
    }
}
