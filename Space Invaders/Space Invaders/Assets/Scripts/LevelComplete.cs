using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    float x = 0;
    float y = 0;
    [SerializeField] float padding = 0;

    public IEnumerator Effect(GameObject effectPrefab, AudioClip efSound,float efSoundVolume)
    {
        Camera camera = Camera.main;
        float height = camera.orthographicSize;
        float width = height * camera.aspect;
        while (true)
        {
            Transform trans = gameObject.transform;
            trans.position = new Vector3(Random.Range(-width + padding, width - padding), Random.Range(-height, height), -1);
            GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity) as GameObject;

            AudioSource.PlayClipAtPoint(efSound, Camera.main.transform.position, efSoundVolume);
            yield return new WaitForSeconds(0.33f); //needs to be syncronized with level transition so it doesn't look horrible. So serialize it.
        }
    }
}
