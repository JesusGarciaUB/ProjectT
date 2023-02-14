using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    Vector2 attackOffset;
    Collider2D swordCollider;
    public int damage = 3;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(transform.parent.position.x + 0.11f, transform.parent.position.y - 0.06f);
        attackOffset = transform.position;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(transform.parent.position.x - 0.11f, transform.parent.position.y - 0.06f);
        attackOffset = transform.position;
    }

    public void AttackUp()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 0.03f);
        attackOffset = transform.position;
    }

    public void AttackDown()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y - 0.2f);
        attackOffset = transform.position;
    }

    public void StopAttack()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y);
        attackOffset = transform.position;
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();

            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
