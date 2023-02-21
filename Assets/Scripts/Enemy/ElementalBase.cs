using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class ElementalBase : EnemyBehaviour
{
    public enum ETYPE { FIRE, WATER, PLANT, RANDOM}
    public ETYPE type;
    SpriteRenderer sr;
    public float MinDistance;
    public float speed;
    public GameObject loot;
    new void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (type == ETYPE.RANDOM)
        {
            type = (ETYPE)Range(0, 3);
        }
        setColor();
        base.Start();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > MinDistance) transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Attack();
    }

    public override void Defeated()
    {
        GameObject droppedLoot = Instantiate(loot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        setLootColor(droppedLoot);
        base.Defeated();
    }

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
                sr.color = Color.green;
                loot.GetComponent<SpriteRenderer>().color = Color.green;
                break;
        }
    }
    void setColor()
    {
        switch (type)
        {
            case ETYPE.FIRE:
                sr.color = Color.red;
                break;
            case ETYPE.WATER:
                sr.color = Color.cyan;
                break;
            case ETYPE.PLANT:
                sr.color = Color.green;
                break;
        }
    }
}
