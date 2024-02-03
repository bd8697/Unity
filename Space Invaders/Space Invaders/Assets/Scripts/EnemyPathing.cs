using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPathing : MonoBehaviour
{
    //config
    WaveConfig waveConfig;
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] bool randomize = false;
    float moveSpeed;
    int waypointIdx = 0;
    [SerializeField] int startAtRandomInScene;


    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetwayPoints();
        if (SceneManager.GetActiveScene().buildIndex == startAtRandomInScene)
        {
            RandomizeList(wayPoints); // very dumb and very inneficient, but it works for this.
        }
        transform.position = wayPoints[waypointIdx].transform.position;
        moveSpeed = waveConfig.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if(waypointIdx <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[waypointIdx].transform.position;
            var toMoveThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, toMoveThisFrame);

            if(transform.position == targetPosition)
            {
                if(randomize)
                {
                    waypointIdx = UnityEngine.Random.Range(1, wayPoints.Count);
                } else
                {
                    waypointIdx++;
                }

            }
        } else
        {
            DestroyMe();
        }
    }

    public void SetWaveConfig(WaveConfig wC)
    {
        this.waveConfig = wC;

    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void RandomizeList(List<Transform> list)
    {
        List<Transform> auxList = list;
        for( int i = 0; i < list.Count; i++)
        {
            var r = UnityEngine.Random.Range(0, list.Count);
            list[i] = auxList[r];
        }
    }
}
