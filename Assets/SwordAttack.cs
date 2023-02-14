using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    Vector2 attackOffset;
    Collider2D swordCollider;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(transform.parent.position.x + 0.09f, transform.position.y - 0.02f);
        attackOffset = transform.position;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(transform.parent.position.x - 0.09f, transform.position.y - 0.02f);
        attackOffset = transform.position;
    }

    public void AttackUp()
    {
        swordCollider.enabled = true;
        transform.position = new Vector3(transform.parent.position.x, transform.position.y);
        attackOffset = transform.position;
    }

    public void AttackDown()
    {
        swordCollider.enabled = true;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }
}
