using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        enemy = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy)
        text.text = enemy.GetHealth().ToString();
    }
}
