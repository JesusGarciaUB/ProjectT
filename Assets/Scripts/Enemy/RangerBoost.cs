using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class RangerBoost : EnemyBehaviour
{
    public float distanceToPlayer;
    private Vector3 rDirection;
    public float speed;
    public GameObject projectile;
    public Transform projectilePos;
    private bool started;
    private float timeToStart;
    private float randTime;
    private Vector3 shootDir;

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
        //player = PersistentManager.Instance.PlayerGlobal;
        og = gameObject.GetComponent<SpriteRenderer>().color;
        timeToStart = 0;
        started = false;
        randTime = Range(1f, 3f);
    }

    private void Update()
    {
        SetLayer();
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
        SetShootDirection();
        if (started && canAttack)
        {
            animator.SetTrigger("isAttack");
            StartCoroutine(StartCooldown());
        }
    }

    private void AttackRange()
    {
        Vector3 newPos = projectilePos.position;
        newPos.z += 100f;
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
        Instantiate(projectile, newPos, Quaternion.identity);

    }

    private void SetSoundAttack()
    {
        GameObject sound = Instantiate(soundAttack);
        Destroy(sound, 2f);
    }

    private void IsRunningAway()
    {
        if (Vector3.Distance(PersistentManager.Instance.PlayerGlobal.transform.position, transform.position) > distanceToPlayer) animator.SetBool("isRunningAway", false);
        else animator.SetBool("isRunningAway", true);
    }

    private void SetShootDirection()
    {
        bool auxMoveX = false;
        bool auxMoveY = false;
        Vector3 dirP = (transform.position - player.transform.position).normalized;
        float y = dirP.y;
        float x = dirP.x;

        if (Mathf.Sign(x) == -1)
        {
            x = -x;
            auxMoveX = true;
        }
        if (Mathf.Sign(y) == -1)
        {
            y = -y;
            auxMoveY = true;
        }
        if (x >= y) y = 0;
        else x = 0;

        shootDir.x = auxMoveX ? -x : x;
        shootDir.y = auxMoveY ? -y : y;
        shootDir = shootDir.normalized;

        animator.SetFloat("shootX", shootDir.x);
        animator.SetFloat("shootY", shootDir.y);
    }
}
