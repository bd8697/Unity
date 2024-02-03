using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    //config params
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplay;

    //state vars
    int pointsPerBlockDestroyed = 69;
    int score = 0;

    void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void KillMe()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
     //   Debug.Log(score);
    }

    public void Score()
    {
        score += pointsPerBlockDestroyed;
        scoreText.text = score.ToString();
    }

    public bool isAutoplayEnabled()
    {
        return autoplay;
    }
}
