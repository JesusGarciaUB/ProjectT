using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : EnemyBehaviour
{
    public float speed;
    //public GameObject loot;

    private void Awake()
    {
        og = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void FixedUpdate()
    {
        SetLayer();
        if (canMove)
        {
            Movement();
            if (!hitting) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            SetAttack();
        }
    }

    public override void Defeated()
    {
        //Instantiate(loot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        base.Defeated();
    }
}
