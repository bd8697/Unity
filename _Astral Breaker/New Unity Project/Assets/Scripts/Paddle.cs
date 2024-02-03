using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float mouseXInUnits;
    [SerializeField] float mouseYInUnits;
    [SerializeField] float screenWidthInUnits = 13.33f;
    [SerializeField] float screenHeightInUnits = 10f;
    [Range(-1f, 1f)][SerializeField] int scale; // -1 = decrease, 0 = stop, 1 = increase
    [SerializeField] float scaleTo;
    [SerializeField] float scaleBy;
    [SerializeField] bool freeControl;
    float leftEdgeOfScreen = 1f;
    float rightEdgeOfScreen = 13.33f - 1.25f;
    float topEdgeOfScreen = 10f;
    float bottomEdgeOfScreen = 0f;
    int screenW = Screen.width;
    int screenH = Screen.height;

    //Cache
    GameStatus game;
    Ball myBall;

    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameStatus>();
        myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame  
    void Update()
    {
        Vector3 paddlePos;
        if (FindObjectOfType<Ball>())
        {
            if (freeControl && FindObjectOfType<Ball>().wasLaunched)
            {
                paddlePos = new Vector3(GetXPos(), GetYPos(), transform.position.z);
            }
            else
            {
                paddlePos = new Vector3(GetXPos(), transform.position.y, transform.position.z);
            }
            transform.position = paddlePos;
        }

        if (scale == -1)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - scaleBy, gameObject.transform.localScale.y - scaleBy, 0f);
            if (gameObject.transform.localScale.x <= scaleTo)
                {
                 scale = 0;
                }
        } else if(scale == 1)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + scaleBy, gameObject.transform.localScale.y + scaleBy, 0f);
            if (gameObject.transform.localScale.x >= scaleTo)
            {
                scale = 0;
            }
        }
    }

    private float GetXPos()
    {
        if(game.isAutoplayEnabled())
        {
            return myBall.transform.position.x;
        } else
        {
            mouseXInUnits = Input.mousePosition.x / screenW * screenWidthInUnits;
            mouseXInUnits = Mathf.Clamp(mouseXInUnits, leftEdgeOfScreen - (1 - gameObject.transform.localScale.x), rightEdgeOfScreen + (1 - gameObject.transform.localScale.x));
            return mouseXInUnits;
        }
    }

    private float GetYPos()
    {
            mouseYInUnits = Input.mousePosition.y / screenH * screenHeightInUnits;
            mouseYInUnits = Mathf.Clamp(mouseYInUnits, bottomEdgeOfScreen - (1 - gameObject.transform.localScale.y), topEdgeOfScreen + (1 - gameObject.transform.localScale.y));
            return mouseYInUnits;
        }
    }
