using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    Vector2 attackOffset;
    Collider2D swordCollider;
    public int damage = 3;
    public float knockbackForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }

    public void AttackRight()
    {
        transform.position = new Vector3(transform.parent.position.x + 0.11f, transform.parent.position.y - 0.06f);
        swordCollider.enabled = true;
        attackOffset = transform.position;

    }

    public void AttackLeft()
    {
        transform.position = new Vector3(transform.parent.position.x - 0.11f, transform.parent.position.y - 0.06f);
        swordCollider.enabled = true;
        attackOffset = transform.position;
    }

    public void AttackUp()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 0.03f);
        swordCollider.enabled = true;
        attackOffset = transform.position;
    }

    public void AttackDown()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y - 0.2f);
        swordCollider.enabled = true;
        attackOffset = transform.position;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y);
        attackOffset = transform.position;        
    }

    private Vector2 Knockback(Collider2D collision)
    {
        Vector3 parentPos = transform.parent.position;
        Vector2 direction = (Vector2)(collision.gameObject.transform.position - parentPos).normalized;
        Vector2 knockBack = direction * knockbackForce;
        return knockBack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitspot")
        {
            EnemyBehaviour enemy = collision.GetComponentInParent<EnemyBehaviour>();
            Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
            IsHitEnemy h = collision.GetComponent<IsHitEnemy>();
            if (enemy != null && enemy.isAlive)
            {
                if (!h.HitEnemy)
                {
                    h.Hitted();
                    rb.AddForce(Knockback(collision));
                    if (enemy.Armor <= 0)
                    {
                        enemy.Health -= damage;
                    }
                    else
                    {
                        enemy.Armor -= damage / 2;
                    }
                }
            }
        }
    }
}
