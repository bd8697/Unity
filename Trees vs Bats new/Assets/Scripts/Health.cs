using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            KillMe();
        }
    }

    public void DealDamage(int dmg)
    {
        health -= dmg;
    }

    public void KillMe()
    {
        Destroy(gameObject);
    }
}
