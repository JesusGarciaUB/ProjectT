using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Follower
{
    private GameObject player;
    private Vector3 direction;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = transform.position - player.transform.position;
        direction.Normalize();
        print(direction);
    }
}
