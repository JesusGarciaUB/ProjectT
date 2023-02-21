using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : EnemyBehaviour
{
    public float speed;
    public GameObject loot;
    protected void FixedUpdate()
    {
        if (hitcount == 0) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public override void Defeated()
    {
        Instantiate(loot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        base.Defeated();
    }
}
