using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if (collision.GetComponent<Ranger>() == null) GetComponentInParent<EnemyBehaviour>().canMoveChecker = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if (collision.GetComponent<Ranger>() == null) GetComponentInParent<EnemyBehaviour>().canMoveChecker = true;
        }
    }
}
