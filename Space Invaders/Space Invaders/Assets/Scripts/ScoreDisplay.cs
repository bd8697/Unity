using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        StartCoroutine(Wait(0.01f));
    }

    IEnumerator Wait(float f) // if we look for gameObject in start(), it gets the wrong one (which will auto-destroy) for some reason, so we first wait for it to be destroyed
    {
        yield return new WaitForSeconds(f);
        text = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = gameSession.GetScore().ToString();
    }
}
