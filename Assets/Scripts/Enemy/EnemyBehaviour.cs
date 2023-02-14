using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
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

}
