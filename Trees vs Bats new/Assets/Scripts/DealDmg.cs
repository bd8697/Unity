using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmg : MonoBehaviour
{
    [SerializeField] int dmg = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collWith)
    {
        collWith.GetComponent<Health>().DealDamage(dmg);
        Destroy(gameObject);
    }
}
