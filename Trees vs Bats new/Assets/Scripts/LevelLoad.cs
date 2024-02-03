using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{

    [SerializeField] float toWait = 3f;
    int currSceneIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        if(currSceneIdx == 0)
        {
            StartCoroutine(LoadStartScreen());
        }
    }

    private IEnumerator LoadStartScreen()
    {
        yield return new WaitForSeconds(toWait);
        LoadNextScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currSceneIdx + 1);
    }
}
