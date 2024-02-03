using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;
    float rnd = 0f;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] GameObject attackerPrefab;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(spawn)
        {
            rnd = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(rnd);
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        Instantiate(attackerPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
