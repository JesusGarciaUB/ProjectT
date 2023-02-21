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
    protected void FixedUpdate()
    {
        if (!hitting) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Attack();
    }
    public override void Defeated()
    {
        base.Defeated();
    }
}
