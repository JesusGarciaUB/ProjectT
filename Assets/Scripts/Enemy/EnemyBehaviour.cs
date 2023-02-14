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

    private void Start()
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

    int health = 1;
    public bool isAlive = true;

    public void Defeated()
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
