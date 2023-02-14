using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : EnemyBehaviour
{
    public float speed;
    private int hitcount;
    public GameObject loot;
    protected void FixedUpdate()
    {
        if (hitcount == 0) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //if (!isAlive) Instantiate(loot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount--;
    }
}
