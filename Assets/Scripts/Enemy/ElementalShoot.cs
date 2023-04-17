using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalShoot : MonoBehaviour
{
    public float timeOnScreen;                                                              //time alive
    public int damage;                                                                      //damage of projectile
    private Rigidbody2D rb;                                                               
    public float speed;                                                                     //speed of projectile
    public float extraRotation;                                                             //extra rotation for sprite accuracity (usually 90)
    private bool canDamage;
    public bool isElemental;
    private void Start()
    {
        Vector3 playerPos = PersistentManager.Instance.PlayerGlobal.transform.position;     //get player position
        playerPos.y -= 0.07f;                                                               //lower vertical position to compensate player hitbox
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = playerPos - transform.position;                                       //set direction of target
        rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;                         //set velocity of projectile

        float rotation = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;                       //calculate angle of shooting
        transform.rotation = Quaternion.Euler(0, 0, rotation + extraRotation);
        canDamage = true;
    }

    void Update()
    {
        SetLayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ProjectileDeleter" && !isElemental) { 
            rb.velocity = new Vector2(0, 0).normalized;
            canDamage = false;
        }
        else if(collision.tag == "ProjectileDeleter" && isElemental)
        {
            Destroy(gameObject);
        }


        if (collision.tag == "EnemyHitspot")
        {
            GameObject p = PersistentManager.Instance.PlayerGlobal;                         //get player
            IsHit obj = p.GetComponentInChildren<IsHit>();                                  //get player hit controller

            if (!obj.Hit && canDamage)
            {
                PlayerController objective = p.GetComponent<PlayerController>();            //logic to damage player
                obj.Hitted();
                objective.Health -= damage;
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject, timeOnScreen);                                              //timer to destroy projectile on set time
        }
    }
    protected void SetLayer()
    {
        int layer = Mathf.FloorToInt((transform.position.y + 0.11f) * 100);
        GetComponent<SpriteRenderer>().sortingOrder = -layer;
    }
}
