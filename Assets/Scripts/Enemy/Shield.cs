using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : EnemyBehaviour
{
    // Update is called once per frame
    private bool changed = false;
    SpriteRenderer sr;
    public GameObject shieldBreak;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        base.Start();

    }

    public float speed;

    private void Update()
    {
        SetLayer();
        if (Armor <= 0 && !changed)
        {
            GameObject soundBreack = Instantiate(shieldBreak);
            Destroy(soundBreack, 2f);
            animator.SetTrigger("noShield");
            //sr.color = Color.red;
            changed = true;
        }
    }
    void FixedUpdate()
    {
        //checkForEnemies();
        if (canMove /*&& canMoveChecker*/)
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
