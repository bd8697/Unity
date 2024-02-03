using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab, gun;


    // Start is called before the first frame update
    void Start()
    {
        gun = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(float dmg)
    {
        Instantiate(projectilePrefab, gun.transform.position, Quaternion.identity);
    }
}
