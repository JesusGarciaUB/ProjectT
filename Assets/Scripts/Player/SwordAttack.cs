using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    Vector2 attackOffset;
    Collider2D swordCollider;
    public int damage = 3;
    public float knockbackForce = 5000f;

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

    private Vector2 Knockback(Collider2D collision)
    {
        Vector3 parentPos = gameObject.GetComponentInParent<Transform>().position;
        Vector2 direction = (Vector2)(collision.gameObject.transform.position - parentPos).normalized;
        Vector2 knockBack = direction * knockbackForce;
        print("Collision" + knockBack);
        return knockBack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                rb.AddForce(Knockback(collision));
                if (enemy.Armor <= 0)
                {
                    enemy.Health -= damage;
                }
                else enemy.Armor -= damage / 2;
            }
        }
    }
}
