using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 100;
    [SerializeField] int score = 100;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] bool canShoot = true;
    [SerializeField] float minTimeTweenShots = 0.1f;
    [SerializeField] float maxTimeTweenShots = 1f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Sounds")]
    [SerializeField] AudioClip deathSound;
    [Range(0f, 1f)][SerializeField] float deathSoundVolume = 0.5f;
    [SerializeField] AudioClip shootSound;
    [Range(0f, 1f)] [SerializeField] float shootSoundVolume = 0.25f;
    
    [Header("VFX")]
    [SerializeField] GameObject HitEffect;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeTweenShots, maxTimeTweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        if(canShoot)
            ShootAfterCooldown();
    }

    private void ShootAfterCooldown()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Shoot();
            shotCounter = UnityEngine.Random.Range(minTimeTweenShots, maxTimeTweenShots);
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        health -= damageDealer.GetDamage();
        damageDealer.OnContact();
        if(health <= 0)
        {
            FindObjectOfType<GameSession>().AddToScore(score);
            FindObjectOfType<Level>().AddToScore(score);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
            Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); //gameObject = this
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
