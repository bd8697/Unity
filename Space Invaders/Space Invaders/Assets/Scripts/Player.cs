using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config
    [Header("Player")]
    [SerializeField] float acceleration = 10f;
    [SerializeField] float paddingX = 0.5f;
    [SerializeField] float paddingY = 0.5f;
    [SerializeField] int health = 3;
    [SerializeField] float invulnerabilityTime = 0.5f;
    [Range(0f, 25f)] [SerializeField] float overheated = 1f;
    [SerializeField] GameObject OverheatingEffect;
    [SerializeField] GameObject HeatingEffect;
    float invulnerabilityTimer = 0f;
    bool isInvulnerable = false;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float firingCooldown = 0.3f;
    [SerializeField] AudioClip deathSound;
    [Range(0f, 5f)] [SerializeField] float deathSoundVolume = 0.5f;
    [SerializeField] AudioClip shootSound;
    [Range(0f, 1f)] [SerializeField] float shootSoundVolume = 0.25f;
    [SerializeField] AudioClip overheatedSound;
    [Range(0f, 5f)] [SerializeField] float hitSoundVolume = 0.5f;
    [SerializeField] AudioClip hitSound;
    [Range(0f, 1f)] [SerializeField] float overheatedSoundVolume = 0.25f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    [SerializeField] float heating = 0;

    [Header("VFX")]
    [SerializeField] GameObject HitEffect;

    Coroutine firingCoroutine;
    GameObject overheatingEffect;
    Color auxColor;

    // Start is called before the first frame update
    void Start()
    {
        SetScreenBoundries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        Muzzle();
        if (overheatingEffect)
        {
            overheatingEffect.transform.position = transform.position;
        }
        IsInvulnerable();
    }

    private void Muzzle()
    {
        auxColor = HeatingEffect.GetComponent<SpriteRenderer>().color;
        auxColor.a = heating / overheated;
        HeatingEffect.GetComponent<SpriteRenderer>().color = auxColor;
        // heatingEffect.GetComponent<SpriteRenderer>().color.a = heating / 5f; // why above works and this doesnt?
        if (Input.GetButton("Fire1"))
        {
            float rnd = UnityEngine.Random.value < 0.5f ? -1 : 1;
            HeatingEffect.transform.Rotate(Vector3.forward * heating / 2 * rnd);
        }
    }

    private void Move()
    {
        //old
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * acceleration;
        var newX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * acceleration;
        var newY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newX, newY);
    }
    private void SetScreenBoundries()
    {
        Camera camera = Camera.main;
        xMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + paddingX;
        xMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - paddingX;
        yMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + paddingY;
        yMax = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - paddingY;
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && !Input.GetButtonUp("Fire1") && heating >= 0) // need to check if button up on same frame as button down to prevent bug when spamming spacebar.
        {
            firingCoroutine = StartCoroutine(Firing());
        }
        if (Input.GetButtonUp("Fire1") || heating < 0)
        {
            StopCoroutine(firingCoroutine);
        }
        if (heating > overheated) //heat up until overheating. At that point, cooldown for overheatedinterval as punishment.
        {
            heating = -overheated / 2;
            overheatingEffect = Instantiate(OverheatingEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(overheatedSound, Camera.main.transform.position, overheatedSoundVolume);
        }
        else if (Input.GetButton("Fire1") || heating < 0)
        {
            heating += Time.deltaTime;
        }
        else if (heating - Time.deltaTime > 0)
        {
            heating -= Time.deltaTime * 3; // cool x times faster than heating
        }
    }

    IEnumerator Firing()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            projectile.GetComponent<ProjectileShootingEffect>().FiringFromObject = gameObject;
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(firingCooldown);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        invulnerabilityTimer += Time.deltaTime;
        if (!isInvulnerable)
        {
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            health -= damageDealer.GetDamage();
            damageDealer.OnContact();
            Instantiate(HitEffect, transform.position, Quaternion.identity);
            if (health <= 0)
            {
                health = 0;
                FindObjectOfType<Level>().LoadGameOver();
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
                Instantiate(HitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject); //gameObject = this
            } else
            {
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, hitSoundVolume);
            }
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void IsInvulnerable () {
        if(invulnerabilityTimer > 0 && invulnerabilityTimer < invulnerabilityTime)
        {
            invulnerabilityTimer += Time.deltaTime;
            isInvulnerable = true;
        } else if(invulnerabilityTimer > invulnerabilityTime)
        {
            invulnerabilityTimer = 0;
        } else
        {
            isInvulnerable = false;
        }
    }


}
