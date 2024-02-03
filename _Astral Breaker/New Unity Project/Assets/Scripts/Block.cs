using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    //config params
    [SerializeField] AudioClip breakBlockSound;
    [SerializeField] GameObject blockParticles;
    [SerializeField] Sprite[] hitSprites;

    // Cached reference
    Level level;
    GameStatus game;

    //state variables
    int timesHit=0;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        AudioSource.PlayClipAtPoint(breakBlockSound, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
        if(tag =="Breakable")
        {
            timesHit++;
            int lives = hitSprites.Length;
            if (timesHit >= lives)
            {
                level.DecrementBreakableBlocks();
                game.Score();
                triggerBlockParticles();
                Destroy(gameObject); // gameObject = this
            }
            else
            {
                ShowNextDamageSprite();
            }
        }
    }

    private void ShowNextDamageSprite()
    {
        if(hitSprites[timesHit - 1] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
        }
    }

    private void triggerBlockParticles()
    {
        GameObject particles = Instantiate(blockParticles, transform.position, transform.rotation);
        Destroy(particles, 3f);
    }

    private void Start()
    {
        if(tag == "Breakable")
        {
            level = FindObjectOfType<Level>();
            level.IncrementBreakableBlocks();
        }
        game = FindObjectOfType<GameStatus>();
    }
}
