using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed;
    public bool isHitting = false;
    void Update()
    {
        if (!isHitting) transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHitspot" || collision.gameObject.tag == "Player") isHitting = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHitspot" || collision.gameObject.tag == "Player") isHitting = false;
    }
}
