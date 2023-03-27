using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected GameObject player;                            
    public float AttackCooldown;                            //cooldown of attack
    public int damage;                                      //own damage, doesn't work if ranged
    public bool canAttack;                                  //auxiliary to know if can attack
    public GameObject healthText;                           //floating damage UI
    public bool canMove;
    private bool isAffected;
    protected Color og;
    protected Animator animator;
    Vector3 previousPosition;
    Vector3 lastMoveDirection;
    //public GameObject PathChecker;
    //public bool canMoveChecker;
    public GameObject soundAttack;

    protected void Start()
    {
        og = PersistentManager.Instance.GetOgColor;
        SetUp();
        player = PersistentManager.Instance.PlayerGlobal;   //get player from global variables
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
        lastMoveDirection = Vector3.zero;
    }

    public void SetUp()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<SpriteRenderer>().color = og;
        player = PersistentManager.Instance.PlayerGlobal;
        isAffected = false;
        canMove = true;
        hitting = false;
        canAttack = true;
        //canMoveChecker = true;
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
    }

    public bool hitting;

    public bool Hitting
    {
        set { hitting = value; }
        get { return hitting; }
    }

    protected void SetAttack()
    {
        if (hitting && canAttack)
        {
            animator.SetTrigger("isAttack");
            StartCoroutine(StartCooldown());
        }
    }
    public void Attack()
    {
        GameObject sound = Instantiate(soundAttack);
        Destroy(sound, 2f);

        if (hitting && !player.GetComponentInChildren<IsHit>().Hit)
        {
            IsHit obj = player.GetComponentInChildren<IsHit>();
            PlayerController objective = player.GetComponent<PlayerController>();
            obj.Hitted();
            objective.Health -= damage;
            print("Attacked: " + damage);
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

    public void Burn(int damage, int ticks)
    {
        if (!isAffected)
        {
            StartCoroutine(VisualEffect(new Color(1.0f, 0.64f, 0.0f), 0.1f, ticks, 0.5f));
            StartCoroutine(WaitForBurn(0.5f, damage, ticks));
        }
    }

    private IEnumerator VisualEffect(Color color, float duration, int loop, float ticktime)
    {
        for (int x = 0; x < loop; x++)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

            sr.color = color;
            yield return new WaitForSeconds(duration);
            sr.color = og;
            yield return new WaitForSeconds(ticktime - duration);
        }
    }
    private IEnumerator VisualEffect(Color color, float duration)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        sr.color = color;
        yield return new WaitForSeconds(duration);
        sr.color = og;
    }

    private IEnumerator WaitForBurn(float duration, int damage, int ticks)
    {
        isAffected = true;
        for (int i = 0; i < ticks; i ++)
        {
            Health -= damage;
            yield return new WaitForSeconds(duration);
        }
        isAffected = false;
    }
    private IEnumerator WaitForFreeze(float duration)
    {
        isAffected = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        isAffected = false;
    }

    public void Stolen(int damage)
    {
        Health -= damage;
    }
    public void Freeze(int duration)
    {
        if (!isAffected)
        {
            StartCoroutine(VisualEffect(Color.blue, duration));
            StartCoroutine(WaitForFreeze(duration));
        }
    }

    protected void Movement()
    {
        if (transform.position != previousPosition)
        {
            lastMoveDirection = (transform.position - previousPosition).normalized;
            previousPosition = transform.position;

            SetMove();

            animator.SetFloat("movementX", lastMoveDirection.x);
            animator.SetFloat("movementY", lastMoveDirection.y);
        }
    }

    private void SetMove()
    {
        bool auxMoveX = false;
        bool auxMoveY = false;
        float y = lastMoveDirection.y;
        float x = lastMoveDirection.x;

        if (Mathf.Sign(x) == -1) { 
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

        lastMoveDirection.x = auxMoveX? -x : x;
        lastMoveDirection.y = auxMoveY ? -y : y; ;
        lastMoveDirection = lastMoveDirection.normalized;
    }
    /*
    protected void checkForEnemies()
    {
        float offset = 0.1f; 
        Vector3 pos = new Vector3();
        if (lastMoveDirection.x != 0)
        {
            pos.y = transform.position.y - 0.09f;
            pos.x = lastMoveDirection.x > 0 ? transform.position.x + offset : transform.position.x - offset;
            PathChecker.transform.position = pos;
        }

        if (lastMoveDirection.y != 0)
        {
            pos.x = transform.position.x;
            pos.y = lastMoveDirection.y > 0 ? (transform.position.y - 0.09f) + offset : (transform.position.y - 0.09f) - offset;
            PathChecker.transform.position = pos;
        }
    }
    */
    public void SetDinamic()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetKinetic()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnLockMovement()
    {
        canMove = true;
    }

    protected void SetLayer()
    {
        int layer = Mathf.FloorToInt((transform.position.y - 0.11f) * 100);
        GetComponent<SpriteRenderer>().sortingOrder = -layer;
    }
}
