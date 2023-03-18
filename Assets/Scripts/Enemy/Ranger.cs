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
        player = PersistentManager.Instance.PlayerGlobal;
        og = gameObject.GetComponent<SpriteRenderer>().color;
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
        SetLayer();
        if (canMove)
        {
            Movement();
            IsRunningAway();
            rDirection = transform.position - player.transform.position;
            rDirection.Normalize();
            transform.position = Vector3.Lerp(transform.position, player.transform.position + rDirection * distanceToPlayer, speed * Time.deltaTime);
            SetAttackRange();
        }
    }

    public override void Defeated()
    {
        base.Defeated();
    }

    private void SetAttackRange()
    {
        if (started && canAttack)
        {
            animator.SetTrigger("isAttack");
            StartCoroutine(StartCooldown());
        }
    }

    private void AttackRange()
    {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }

    private void IsRunningAway()
    {
        if (Vector3.Distance(PersistentManager.Instance.PlayerGlobal.transform.position, transform.position) > distanceToPlayer) animator.SetBool("isRunningAway", false);
        else animator.SetBool("isRunningAway", true);
    }
}
