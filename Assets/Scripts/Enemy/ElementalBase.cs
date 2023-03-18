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
    public Transform projectilePos;                                 //center of elemental, from where projectiles spawn
    new void Start()
    {
        if (type == ETYPE.RANDOM)                                   //random type if set to do so
        {
            type = (ETYPE)Range(0, 3);
        }
        base.Start();
    }
    private void FixedUpdate()
    {
        SetLayer();
        if (canMove)
        {
            //follow target if not in min distance
            if (Vector3.Distance(player.transform.position, transform.position) > MinDistance) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            else Attack();  //attacks if in range
        }
    }

    /// <summary>
    ///Logic behind being defeated 
    /// </summary>
    public override void Defeated()
    {
        GameObject droppedLoot = Instantiate(loot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);    //instance of loot dropped
        setLootColor(droppedLoot);                                                                                                      //set color of loot to own color
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
        if (canAttack)
        {
            GameObject p = Instantiate(projectile, projectilePos.position, Quaternion.identity);    //instance of projectile
            SpriteRenderer sr2 = p.GetComponent<SpriteRenderer>();                                  //set color of projectile to own         
            StartCoroutine(StartCooldown());                                                      //start cooldown of own attack
        }
    }
}
