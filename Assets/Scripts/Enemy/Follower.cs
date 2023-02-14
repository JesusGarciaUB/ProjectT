using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : EnemyBehaviour
{
    public float speed;
    private int hitcount;
    protected Transform target;
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    protected void FixedUpdate()
    {
        if (hitcount == 0) transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
