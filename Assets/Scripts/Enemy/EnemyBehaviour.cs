using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed;
    private int hitcount;
    private void FixedUpdate()
    {
        if (hitcount == 0) transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().transform.position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") hitcount--;
    }
}
