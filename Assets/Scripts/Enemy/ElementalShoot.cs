using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalShoot : MonoBehaviour
{
    public float timeOnScreen;
    public int damage;
    private Rigidbody2D rb;
    public float speed;
    public float extraRotation;
    private void Start()
    {
        Vector3 playerPos = PersistentManager.Instance.PlayerGlobal.transform.position;     //get player position
        playerPos.y -= 0.07f;                                                               //lower vertical position to compensate player hitbox
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = playerPos - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;

        float rotation = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + extraRotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyHitspot")
        {
            GameObject p = PersistentManager.Instance.PlayerGlobal;
            IsHit obj = p.GetComponentInChildren<IsHit>();

            if (!obj.Hit)
            {
                PlayerController objective = p.GetComponent<PlayerController>();
                obj.Hitted();
                objective.Health -= damage;
                print("Attacked: " + damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject, timeOnScreen);
        }
    }
}
