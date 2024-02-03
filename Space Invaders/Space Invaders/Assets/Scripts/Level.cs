using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] AudioSource dontDestroyMe;
    [SerializeField] GameObject effect;
    SceneLoaderScript sceneloader;
    LevelComplete levelComplete;
    [SerializeField] AudioClip effectSound;
    [Range(0f, 1f)] [SerializeField] float effectSoundVolume = 0.25f;
    private int levelScore = 0;

    private delegate bool IsLevelOverDelegate();
    IsLevelOverDelegate lvlOvr;

    void Start()
    {
        lvlOvr = IsLevelOver;
        if (FindObjectsOfType<AudioSource>().Length == 1 && dontDestroyMe)
            DontDestroyOnLoad(dontDestroyMe);
        sceneloader = FindObjectOfType<SceneLoaderScript>();
        levelComplete = FindObjectOfType<LevelComplete>();
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "StartMenu" && SceneManager.GetActiveScene().name != "Game Over" && lvlOvr()) // delegate recieves method that checks for level over then becomes null
        {
            if(levelComplete) StartCoroutine(levelComplete.Effect(effect, effectSound, effectSoundVolume));
            if(SceneManager.GetActiveScene().name != "Congrats!")
            {
                StartCoroutine(DelayNextScene());
            }
            //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            lvlOvr = ImAlwaysFalse;
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainGame() //refactor. Block Breaker for refference.
    {
        SceneManager.LoadScene("Level 1");
        FindObjectOfType<GameSession>().ResetGame();
        FindObjectOfType<GameSession>().ResetHealth();
    }

    public void LoadLastScene()
    {
        SceneManager.LoadScene(FindObjectOfType<GameSession>().lastSceneIdx);
        //  FindObjectOfType<GameSession>().ResetGame();
        FindObjectOfType<GameSession>().ResetHealth();
    }

    public void LoadGameOver()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<GameSession>().lastSceneIdx = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(DelayGameOver());
    }

    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<GameSession>().DecreaseScore(levelScore);
        SceneManager.LoadScene("Game Over");
    }
    IEnumerator DelayNextScene()
    {
        yield return new WaitForSeconds(5f);
      //  SceneManager.LoadScene("Level 5");
         sceneloader.LoadNextScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private bool IsLevelOver()
    {
        if(FindObjectsOfType<Enemy>().Length == 0)
        {
            return true;
        }
        return false;
    }

    private bool ImAlwaysFalse()
    {
        return false;
    }

    public void AddToScore(int toAdd)
    {
        levelScore += toAdd;
    }
}

// Update is called once per frame
