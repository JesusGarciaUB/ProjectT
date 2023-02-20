using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    public Transform fireDirection;
    public GameObject arrowPrefab;
    public float speed = 20f;
    public int damage = 1;

    // Update is called once per frame
    public void AttackRight()
    {
        Instantiate(arrowPrefab);
    }

    public void AttackLeft()
    {
        
    }

    public void AttackUp()
    {
        
    }

    public void AttackDown()
    {
        
    }

    public void StopAttack()
    {
        
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
