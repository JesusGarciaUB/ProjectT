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

    protected void Start()
    {
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
}
