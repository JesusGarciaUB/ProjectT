using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class ElementalBase : EnemyBehaviour
{
    public enum ETYPE { FIRE, WATER, PLANT, RANDOM}                 //possible types of elemental
    public ETYPE type;                                              //type of current elemental
    public float MinDistance;                                       //minimum distance to target to shoot
    public float speed;                                             //movespeed of elemental
    public GameObject loot;                                         //loot drop
    //shooting  
    public GameObject projectile;                                   //type of projectile, prefab
    public Transform projectilePos;
    private Vector3 shootDir;
    [SerializeField] private bool isLast;
    new void Start()
    {
        if (type == ETYPE.RANDOM)                                   //random type if set to do so
        {
            type = (ETYPE)Range(0, 3);
        }
        base.Start();
    }

    private void Update()
    {
        SetLayer();

        if (canMove && !isFrozen)
        {
            //follow target if not in min distance
            if (Vector3.Distance(player.transform.position, transform.position) > MinDistance)
            {
                animator.SetBool("moving", true);
                Movement();
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("moving", false);
                SetMoveIdle();
                SetterAttack();
            }
        }

    }

    /// <summary>
    ///Logic behind being defeated 
    /// </summary>
    public override void Defeated()
    {
        GameObject droppedLoot = Instantiate(loot, new Vector3(transform.position.x, transform.position.y - 0.11f, 0), Quaternion.identity);    //instance of loot dropped
        setLootColor(droppedLoot);                                                                                                      //set color of loot to own color
        //Destroy(transform.GetChild(1).gameObject);
        base.Defeated();
    }

    /// <summary>
    /// set loot to own color based on type
    /// </summary>
    /// <param name="loot">loot object reference</param>
    void setLootColor(GameObject loot)
    {
        switch (type)
        {
            case ETYPE.FIRE:
                loot.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case ETYPE.WATER:
                loot.GetComponent <SpriteRenderer>().color = Color.cyan;
                break;
            case ETYPE.PLANT:
                loot.GetComponent<SpriteRenderer>().color = Color.green;
                break;
        }
    }

    /// <summary>
    /// logic of attack
    /// </summary>
    new private void Attack()
    {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);    //instance of projectile     
        GameObject sound = Instantiate(soundAttack);
        Destroy(sound, 2f);
    }
    
    private void EndAttackAnimation()
    {
        animator.SetTrigger("shooting");
        canMove = true;
    }
    private void SetterAttack()
    {
        if (canAttack && !isFrozen)
        {
            animator.SetTrigger("shooting");
            StartCoroutine(StartCooldown());
        }
    }
    private void SetMoveIdle()
    {
        bool auxMoveX = false;
        bool auxMoveY = false;
        Vector3 dirP = (transform.position - player.transform.position).normalized;
        float y = dirP.y;
        float x = dirP.x;

        if (Mathf.Sign(x) == -1)
        {
            x = -x;
            auxMoveX = true;
        }
        if (Mathf.Sign(y) == -1)
        {
            y = -y;
            auxMoveY = true;
        }
        if (x >= y) y = 0;
        else x = 0;

        shootDir.x = auxMoveX ? -x : x;
        shootDir.y = auxMoveY ? -y : y;
        shootDir = shootDir.normalized;

        animator.SetFloat("movementX", shootDir.x);
        animator.SetFloat("movementY", shootDir.y);
    }
}
