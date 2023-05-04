using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeam : MonoBehaviour
{
    public int damage;
    public GameObject sound;

    public void SetSound()
    {
        GameObject cosa = Instantiate(sound);
        Destroy(cosa, 1.5f);
    }

    protected void DeactivateCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    protected void ActivateCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyHitspot")
        {
            GameObject p = PersistentManager.Instance.PlayerGlobal;                         //get player
            IsHit obj = p.GetComponentInChildren<IsHit>();                                  //get player hit controller

            if (!obj.Hit)
            {
                PlayerController objective = p.GetComponent<PlayerController>();            //logic to damage player
                obj.Hitted();
                objective.Health -= damage;
            }
        }
    }
}
