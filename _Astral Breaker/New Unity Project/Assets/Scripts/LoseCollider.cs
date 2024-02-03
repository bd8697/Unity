using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject); 
        Ball[] balls = FindObjectsOfType<Ball>();
        Debug.Log(balls.Length);
        if (balls.Length == 1) //todo: why 1 and not 0? Is there a hidden ball (lol) in the scene?
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
