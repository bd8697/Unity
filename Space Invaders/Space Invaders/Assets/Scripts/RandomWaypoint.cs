using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWaypoint : MonoBehaviour
{
    //config
    WaveConfig waveConfig;
    [SerializeField] List<Transform> wayPoints;
    float moveSpeed;
    int waypointIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetwayPoints();
        transform.position = wayPoints[waypointIdx].transform.position;
        moveSpeed = waveConfig.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
            Debug.Log(wayPoints.Count);
            Debug.Log(waypointIdx);
            var targetPosition = wayPoints[waypointIdx].transform.position;
            var toMoveThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, toMoveThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIdx = Random.Range(1, wayPoints.Count);
            }
    }

    public void SetWaveConfig(WaveConfig wC)
    {
        this.waveConfig = wC;

    }
}
