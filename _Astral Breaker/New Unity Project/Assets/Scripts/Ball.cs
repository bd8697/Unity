using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xLaunch = 0f;
    [SerializeField] float yLaunch = 15f;
    [SerializeField] float rndFactor = 0.2f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] bool increaseSizeOnHit;
    [SerializeField] float increaseFactor;

    // state
    Vector3 paddleToBallVector;
    public Rigidbody2D rb;
    int angVelSpeed = -1000; // in deg/sec
    public bool wasLaunched = false;

    //Cached components
    AudioSource audioSrc;
    Rigidbody2D myRigidBody2D;

    private void LockBallToPaddle()
    {
        Vector3 paddlePos = new Vector3(paddle1.transform.position.x, paddle1.transform.position.y, paddle1.transform.position.z);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnClick()
    {
      if(Input.GetMouseButtonDown(0)) // 0 represents left click
        {
            wasLaunched = true;
            xLaunch = Random.Range(-3f, 3f);
            myRigidBody2D.velocity = new Vector3(xLaunch, yLaunch, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velDelta = new Vector2(Random.Range(0, rndFactor), Random.Range(0, rndFactor));
        if(wasLaunched)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            audioSrc.PlayOneShot(clip); //playoneshot = don't intrerupt it while palying
           // myRigidBody2D.velocity += velDelta;
           if(increaseSizeOnHit)
            {
                gameObject.transform.localScale = new Vector3(increaseFactor + gameObject.transform.localScale.x, increaseFactor + gameObject.transform.localScale.y, 0f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = angVelSpeed;
        audioSrc = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!wasLaunched)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
        
    }
}
