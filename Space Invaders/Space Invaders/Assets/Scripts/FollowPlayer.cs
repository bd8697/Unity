using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(player)
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + player.GetComponent<Renderer>().bounds.size.y / 4);
    }
}
