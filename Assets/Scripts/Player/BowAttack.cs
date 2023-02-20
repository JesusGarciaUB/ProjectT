using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float speed = 5f;
    public int damage = 1;

    // Update is called once per frame
    public void Attack()
    {
        GameObject arrow = Instantiate(arrowPrefab, PersistentManager.Instance.PlayerGlobal.transform.position, PersistentManager.Instance.PlayerGlobal.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(PersistentManager.Instance.PlayerGlobal.transform.up * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyBehaviour enemy = collision.GetComponent<EnemyBehaviour>();

            if (enemy != null)
            {
                if (enemy.Armor <= 0)
                {
                    enemy.Health -= damage;
                }
                else enemy.Armor -= damage / 2;
            }
        }
    }

}
