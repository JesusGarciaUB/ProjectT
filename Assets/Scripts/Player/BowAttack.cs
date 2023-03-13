using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float speed = 3f;
    public float arrowTimeOnScreen = 4f;
    public int damage = 1;
    private Quaternion rotation;
    private Vector3 trans;
    private Vector3 position;

    // Update is called once per frame
    public void Attack()
    {
        ArrowDir();                                                                 //Check player rotation and position
        GameObject arrow = Instantiate(arrowPrefab, position, rotation);            //Instance arrow
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce( trans * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHitspot")
        {
            EnemyBehaviour enemy = collision.GetComponentInParent<EnemyBehaviour>();

            if (enemy != null)
            {
                if (enemy.Armor <= 0)
                {
                    enemy.Health -= damage;
                }
                else enemy.Armor -= damage / 2;

                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject, arrowTimeOnScreen);
        }
    }

    private void ArrowDir() //Set arrow direction and position
    {
        position = PersistentManager.Instance.PlayerGlobal.transform.position;
        switch (PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().dir) //Check player facing direction to set arrow direction and position
        {
            case PlayerController.Direction.UP:
                rotation = Quaternion.Euler(0, 0, 90);                                        //Set arrow facing up
                trans = PersistentManager.Instance.PlayerGlobal.transform.up;
                position.y = position.y + 0.11f;
                break;
            case PlayerController.Direction.DOWN:
                rotation = Quaternion.Euler(0, 0, -90);
                trans = PersistentManager.Instance.PlayerGlobal.transform.up * -1;
                position.y = position.y - 0.20f;
                break;
            case PlayerController.Direction.LEFT:
                trans = PersistentManager.Instance.PlayerGlobal.transform.right * -1;
                rotation = Quaternion.Euler(0, 0, 180);
                position.x = position.x + 0.04f;
                break;
            case PlayerController.Direction.RIGHT:
                trans = PersistentManager.Instance.PlayerGlobal.transform.right;
                rotation = Quaternion.Euler(0, 0, 0);
                position.x = position.x + 0.04f;
                break;

        }
    }

}
