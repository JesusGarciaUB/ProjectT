using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float speed = 20f;
    public int damage = 1;
    private Quaternion rotation;
    private Vector3 trans;

    // Update is called once per frame
    public void Attack()
    {
        ArrowDir();//Check player rotation
        GameObject arrow = Instantiate(arrowPrefab, PersistentManager.Instance.PlayerGlobal.transform.position, rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce( trans * speed, ForceMode2D.Impulse);
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

    private void ArrowDir()
    {
        switch(PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().dir)
        {
            case PlayerController.Direction.UP:
                rotation = Quaternion.Euler(0, 0, 90);
                trans = PersistentManager.Instance.PlayerGlobal.transform.up;
                break;
            case PlayerController.Direction.DOWN:
                rotation = Quaternion.Euler(0, 0, -90);
                trans = PersistentManager.Instance.PlayerGlobal.transform.up * -1;
                break;
            case PlayerController.Direction.LEFT:
                trans = PersistentManager.Instance.PlayerGlobal.transform.right * -1;
                rotation = Quaternion.Euler(0, 0, 180);
                break;
            case PlayerController.Direction.RIGHT:
                trans = PersistentManager.Instance.PlayerGlobal.transform.right;
                rotation = Quaternion.Euler(0, 0, 0);
                break;

        }
    }

}
