using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explodable")
        {
            collision.GetComponent<ExplodableBehavior>().Explode();
            Destroy(gameObject);
        }
    }
}
