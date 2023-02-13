using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : EnemyBehaviour
{

    private GameObject player;
    public float distanceToPlayer;
    private Vector3 direction;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = transform.position - player.transform.position;
        direction.Normalize();
        transform.position = Vector3.Lerp(transform.position, player.transform.position + direction * distanceToPlayer, speed * Time.deltaTime);
    }
}
