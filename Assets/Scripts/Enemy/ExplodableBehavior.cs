using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableBehavior : MonoBehaviour
{
    public GameObject sound;
    private void Start()
    {
        SetLayer();
    }
    public void Explode()
    {
        GetComponent<Animator>().SetTrigger("Explode");
        GameObject toexplode = Instantiate(sound);
        Destroy(toexplode, 2f);
    }

    public void FinishAnimation()
    {
        Destroy(gameObject);
    }

    protected void SetLayer()
    {
        int layer = Mathf.FloorToInt((transform.position.y - 0.11f) * 100);
        GetComponent<SpriteRenderer>().sortingOrder = -layer;
    }
}
