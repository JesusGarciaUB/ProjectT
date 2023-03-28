using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTestGround : MonoBehaviour
{

    private Vector3 position = new Vector3(0, 0, -10);
    // Start is called before the first frame update

    private void Awake()
    {
        position = transform.position;
        position.z = -10;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Follower").transform.position = position;
        }
    }
}
