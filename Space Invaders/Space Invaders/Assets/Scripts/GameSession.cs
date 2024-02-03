    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score; //ser for debugging
    int health = 3;
    public int lastSceneIdx;

    void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            score = 0;
        }
    }
    
    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int toAdd)
    {
        score += toAdd;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }

    public void DecreaseHealth(int minus)
    {
        health -= minus;
    }

    public void DecreaseScore(int minusScore)
    {
        score -= minusScore;
    }

    public void ResetHealth()
    {
        health = 3;
    }
}
