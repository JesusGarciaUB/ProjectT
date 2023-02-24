using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //to get angle
    private Vector3 direction;
    float angle;
    protected enum Facing {NOONE, UP, DOWN, LEFT, RIGHT };
    protected Facing dir = Facing.NOONE;
    protected GameObject player;
    public float AttackCooldown;
    public int damage;
    public bool canAttack;
    public GameObject healthText;

    protected void Start()
    {
        hitting = false;
        canAttack = true;
        player = PersistentManager.Instance.PlayerGlobal;
    }
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
                print("Enemy current healt:" + health + " Damage: " + damage);
                textMesh.SetText(damage.ToString());

                //Set health loss text inside the canvas
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            health = value;
            if (health <= 0) Defeated();
        }
        get
        {
            return health;
        }
    }

    public int Armor
    {
        set
        {
            armor = value;
        }
        get
        {
            return armor;
        }
    }

    int health = 4;
    int armor = 0;
    public bool isAlive = true;

    virtual public void Defeated()
    {
        isAlive = false;
        transform.gameObject.SetActive(false);
    }

    public float getAngle()
    {
        direction = transform.position - player.transform.position;
        direction.Normalize();
        direction = player.transform.InverseTransformDirection(direction);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
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
