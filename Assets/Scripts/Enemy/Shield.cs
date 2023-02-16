using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : EnemyBehaviour
{
    // Update is called once per frame
    private void Awake()
    {
        Armor = 2;

        base.Start();

    }

    public float speed;
    private int hitcount;
    protected void FixedUpdate()
    {
        if (hitcount == 0) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount--;
    }

    public override void Defeated()
    {
        base.Defeated();
    }
}
