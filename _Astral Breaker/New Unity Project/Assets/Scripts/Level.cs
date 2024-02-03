using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocksLen = 0;//Serialized just for debugging

    //Chached
    SceneLoaderSCript sceneloader;
   public void IncrementBreakableBlocks()
    {
        breakableBlocksLen++;
    }

    public void DecrementBreakableBlocks()
    {
        breakableBlocksLen--;
    }

    public bool AllBlocksDestroyed()
    {
        if(breakableBlocksLen == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneloader = FindObjectOfType<SceneLoaderSCript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AllBlocksDestroyed())
        {
            sceneloader.LoadNextScene();
          //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
