using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField] float degPerSecond;
    [SerializeField] bool rndDir;

    int dir;
    // Start is called before the first frame update
    void Start()
    {
        if(rndDir)
        {
            dir = Random.Range(0, 2) == 0 ? 1 : -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, degPerSecond * Time.deltaTime * dir);
    }
}
