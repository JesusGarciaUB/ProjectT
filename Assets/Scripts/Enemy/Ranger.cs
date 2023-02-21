using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : EnemyBehaviour
{
    public float distanceToPlayer;
    private Vector3 rDirection;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        rDirection = transform.position - player.transform.position;
        rDirection.Normalize();
        transform.position = Vector3.Lerp(transform.position, player.transform.position + rDirection * distanceToPlayer, speed * Time.deltaTime);
        Attack();
    }

    public override void Defeated()
    {
        base.Defeated();
    }
}
