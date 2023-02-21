using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitspot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyBehaviour attacker = collision.gameObject.GetComponent<EnemyBehaviour>();
            attacker.Hitting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyBehaviour attacker = collision.gameObject.GetComponent<EnemyBehaviour>();
            attacker.Hitting = false;
        }
    }
}
