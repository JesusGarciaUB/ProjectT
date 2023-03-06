using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Ranger : EnemyBehaviour
{
    public float distanceToPlayer;
    private Vector3 rDirection;
    public float speed;
    public GameObject projectile;
    public Transform projectilePos;
    private bool started;
    private float timeToStart;
    private float randTime;

    public bool StartedP
    {
        set { started = value; }
    }
    public float TimeTo
    {
        set { timeToStart = value; }
    }
    private void Awake()
    {
        timeToStart = 0;
        started = false;
        randTime = Range(1f, 3f);
    }

    private void Update()
    {
        if (!started) timeToStart += Time.deltaTime;
        if (timeToStart > randTime && !started)
        {
            started = true;
        }
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            rDirection = transform.position - player.transform.position;
            rDirection.Normalize();
            transform.position = Vector3.Lerp(transform.position, player.transform.position + rDirection * distanceToPlayer, speed * Time.deltaTime);
            Attack();
        }
    }

    public override void Defeated()
    {
        base.Defeated();
    }

    new private void Attack()
    {
        if (canAttack && started)
        {
            Instantiate(projectile, projectilePos.position, Quaternion.identity);
            StartCoroutine(StartCooldown());
        }
    }
}
