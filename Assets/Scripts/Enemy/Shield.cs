using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : EnemyBehaviour
{
    // Update is called once per frame
    private bool changed = false;
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Armor = 2;

        base.Start();

    }

    public float speed;

    private void Update()
    {
        SetLayer();
        if (Armor <= 0 && !changed)
        {
            sr.color = Color.red;
            changed = true;
        }

    }
    protected void FixedUpdate()
    {
        if (canMove)
        {
            Movement();
            if (!hitting) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            SetAttack();
        }
    }
    public override void Defeated()
    {
        base.Defeated();
    }
}
