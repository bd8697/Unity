using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSCript : MonoBehaviour
{
    int currSceneIdx;
    public void LoadNextScene()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx + 1);
        if (SceneManager.GetActiveScene().name == "Game Over")
        {
            Cursor.visible = true;
        }
    }

    public void LoadStartScene()
    {
        GameStatus game = FindObjectOfType<GameStatus>();
        game.KillMe();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
