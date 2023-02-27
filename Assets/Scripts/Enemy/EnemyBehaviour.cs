using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected enum Facing {NOONE, UP, DOWN, LEFT, RIGHT }; //possible direction of facing
    protected Facing dir = Facing.NOONE;                    //current direction of facing
    protected GameObject player;                            
    public float AttackCooldown;                            //cooldown of attack
    public int damage;                                      //own damage, doesn't work if ranged
    public bool canAttack;                                  //auxiliary to know if can attack
    public GameObject healthText;                           //floating damage UI

    protected void Start()
    {
        hitting = false;
        canAttack = true;
        player = PersistentManager.Instance.PlayerGlobal;   //get player from global variables
    }

    /// <summary>
    /// set health based on value received, includes logic for floating damage 
    /// </summary>
    public int Health
    {
        set{
            if (value < health)
            {
                //Set health loss text position on top of the enemy
                GameObject gm = Instantiate(healthText);
                RectTransform textTransform = gm.GetComponent<RectTransform>();
                Vector3 v3 = transform.position;
                v3.y += 0.16f;
                textTransform.transform.position = v3;

                //Add damage dealet
                TextMeshProUGUI textMesh = gm.GetComponent<TextMeshProUGUI>();
                int damage = health - value;
                textMesh.SetText(damage.ToString());

                //Set health loss text inside the canvas
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            health = value;
            if (health <= 0) Defeated();                                        //calling defeated void if health value <= 0
        }
        get
        {
            return health;
        }
    }

    /// <summary>
    /// set armor based on value received, includes logic for floating damage
    /// </summary>
    public int Armor
    {
        set
        {
            if (value < armor)
            {
                //Set health loss text position on top of the enemy
                GameObject gm = Instantiate(healthText);
                RectTransform textTransform = gm.GetComponent<RectTransform>();
                Vector3 v3 = transform.position;
                v3.y += 0.16f;
                textTransform.transform.position = v3;

                //Add damage dealet
                TextMeshProUGUI textMesh = gm.GetComponent<TextMeshProUGUI>();
                int damage = armor - value;
                textMesh.SetText(damage.ToString());
                textMesh.color = Color.yellow;

                //Set health loss text inside the canvas
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            armor = value;
        }
        get
        {
            return armor;
        }
    }

    public int health;
    public int armor;
    public bool isAlive = true;

    virtual public void Defeated()
    {
        isAlive = false;
        transform.gameObject.SetActive(false);
        PersistentManager.Instance.EnemiesRemaining--;
        if (PersistentManager.Instance.EnemiesRemaining == 0)
        {
            PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().Win();
        }
    }

    public bool hitting;

    public bool Hitting
    {
        set { hitting = value; }
        get { return hitting; }
    }
    public void Attack()
    {
        if (hitting && canAttack && !player.GetComponentInChildren<IsHit>().Hit)
        {
            IsHit obj = player.GetComponentInChildren<IsHit>();
            PlayerController objective = player.GetComponent<PlayerController>();
            obj.Hitted();
            objective.Health -= damage;
            print("Attacked: " + damage);
            StartCoroutine(StartCooldown());
        }
    }
    public IEnumerator StartCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHitspot")
        {
            Hitting = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHitspot")
        {
            Hitting = false;
        }
    }
}
