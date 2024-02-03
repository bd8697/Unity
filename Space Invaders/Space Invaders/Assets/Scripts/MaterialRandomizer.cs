using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomizer : MonoBehaviour
{
    int rndNr;
    bool triggered = false;
    [SerializeField] Material mat1;
    [SerializeField] Material mat2;
    [SerializeField] Material mat3;
    [SerializeField] Material mat4;
    [SerializeField] Material mat5;

    void Update()
    {
        if (gameObject.GetComponent<ParticleSystem>().particleCount > 0 && triggered == false)
        {
            Debug.Log("yoyoy");
            rndNr = Random.Range(1, 6);
            switch (rndNr)
            {
                case 1:
                    gameObject.GetComponent<ParticleSystemRenderer>().material = mat1; break;
                case 2:
                    gameObject.GetComponent<ParticleSystemRenderer>().material = mat2; break;
                case 3:
                    gameObject.GetComponent<ParticleSystemRenderer>().material = mat3; break;
                case 4:
                    gameObject.GetComponent<ParticleSystemRenderer>().material = mat4; break;
                case 5:
                    gameObject.GetComponent<ParticleSystemRenderer>().material = mat5; break;
            }
            triggered = true;
        }

        if (gameObject.GetComponent<ParticleSystem>().particleCount == 0 && triggered == true)
        {
            triggered = false;
        }
    }
}
