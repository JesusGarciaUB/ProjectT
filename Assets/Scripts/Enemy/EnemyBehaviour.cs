using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //to get angle
    private Vector3 direction;
    float angle;
    protected enum Facing {NOONE, UP, DOWN, LEFT, RIGHT };
    protected Facing dir = Facing.NOONE;
    protected GameObject player;
    public float AttackCooldown;
    private float Cooldown;
    public int damage;

    protected void Start()
    {
        Cooldown = Time.time;
        player = GameObject.FindWithTag("Player");
    }
    public int Health
    {
        set{ 
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public int Armor
    {
        set
        {
            armor = value;
        }
        get
        {
            return armor;
        }
    }

    int health = 1;
    int armor = 0;
    public bool isAlive = true;

    virtual public void Defeated()
    {
        isAlive = false;
        transform.gameObject.SetActive(false);
    }

    float getAngle()
    {
        direction = transform.position - player.transform.position;
        direction.Normalize();
        direction = player.transform.InverseTransformDirection(direction);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }

    public int hitcount;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount++;

        if (hitcount > 0)
        {
            PlayerController objective = collision.collider.GetComponent<PlayerController>();
            if (Cooldown <= Time.time)
            {
                objective.Health -= damage;
                Cooldown = Time.time + AttackCooldown;
                print("Attacked: " + damage);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount--;
    }
}
