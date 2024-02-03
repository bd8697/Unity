using UnityEngine;
using System.Collections;

public class ProjectileShootingEffect : MonoBehaviour {
	public GameObject ShootEffect;
	public GameObject FiringFromObject;

    GameObject shotEffect;

    // Use this for initialization
    void Start () {
		shotEffect = Instantiate(ShootEffect, transform.position, Quaternion.identity) as GameObject; //Spawn muzzle flash
    }
	
	// Update is called once per frame
	void Update () {

    }
	
	void OnTriggerEnter2D(Collider2D col) {
		Destroy(gameObject);
	}
}
