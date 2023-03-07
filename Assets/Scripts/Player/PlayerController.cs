using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT }; 
    public enum WEAPON{ SWORD, BOW };
    public enum MAGICe { NOONE, FIRE, ICE, PLANT};
    private MAGICe currentMagic = MAGICe.NOONE;
    public Direction dir = Direction.NONE;                  //Used to save player last direction in dir
    public WEAPON weapon;                                   //Last weapon used
    public float speed = 1f;                                //Player speed
    public float collOffset = 0.02f;
    public ContactFilter2D cF;
    public SwordAttack swordAttack;
    public BowAttack bowAttack;
    Vector2 playerMovement;
    Animator animator;
    Rigidbody2D rb;
    public List<RaycastHit2D> cColl = new List<RaycastHit2D>();
    bool canMove = true;                                    //On true player can move
    public GameObject healthText;                           //Displays damage on player
    int health = 100;
    private int maxHealth;
    public GameObject spells;
    private bool healed = false;
    public int MagicCooldown;
    private bool CanMagic;
    public InteractionScreen Ins;
    private bool Interacting;

    // Start is called before the first frame update
    void Start()
    {
        Interacting = false;
        CanMagic = true;
        maxHealth = health;
        dir = Direction.DOWN;                               //By default player face down
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PersistentManager.Instance.hp.SetMaxHealth(health);
    }

    public bool InteractingSetter
    {
        set { Interacting = value; }
    }
    public MAGICe MagicSetter
    {
        get { return currentMagic; }
        set { currentMagic = value; }
    }

    public int Health
    {
        get { return health; }
        set 
        {
            if (value < health || healed)
            {
                //Set health loss text position on top of the enemy
                Vector3 v3 = transform.position;
                v3.y += 0.16f;
                GameObject gm = Instantiate(healthText, v3, Quaternion.identity);   //moved from -=3 lines
                RectTransform textTransform = gm.GetComponent<RectTransform>();     //
                //textTransform.transform.position = v3;                            //set in instantiate

                //Add damage dealet
                TextMeshProUGUI textMesh = gm.GetComponent<TextMeshProUGUI>();
                int damage = health - value;
                //if (!healed) textMesh.SetText(damage.ToString());
                //else textMesh.SetText(damage.ToString()[1].ToString());
                if (!healed) textMesh.text = damage.ToString();
                else textMesh.text = damage.ToString()[1].ToString();
                if (healed) textMesh.color = Color.green;

                //Set health loss text inside the canvas
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);

                PersistentManager.Instance.hp.SetHealth(damage);
            }

            health = value; 
            if (health <= 0) Defeated();
        } 
    }
    private void Defeated()
    {
        PersistentManager.Instance.winlose.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        PersistentManager.Instance.winlose.GetComponent<TMP_InputField>().text = "YOU WIN!!";
        PersistentManager.Instance.winlose.SetActive(true);
        Time.timeScale = 0f;
    }
    private void FixedUpdate()
    {
        SetLayer();
        SetLastPosition();
        if (canMove)
        {
            
            if (playerMovement != Vector2.zero)
            {
                animator.SetFloat("movementX", playerMovement.x);
                animator.SetFloat("movementY", playerMovement.y); 
                animator.SetBool("isMoving", true);
                bool success = TryMove(playerMovement);

                if (!success)
                {
                    success = TryMove(new Vector2(playerMovement.x, 0));

                    if (!success) success = TryMove(new Vector2(0, playerMovement.y));
                }

            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
               direction,
               cF,
               cColl,
               speed * Time.deltaTime + collOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        } else return false;
    }

    void OnMove(InputValue movement)
    {
        playerMovement = movement.Get<Vector2>();
    }

    //Save player facing last position
    void SetLastPosition()
    {
        if (canMove)
        {
            //Facing up
            if (playerMovement.y > 0)
            {
                dir = Direction.UP;
                animator.SetFloat("positionY", 1);
                animator.SetFloat("positionX", 0);
            }
            //Facing right
            if (playerMovement.x > 0)
            {
                dir = Direction.RIGHT;
                animator.SetFloat("positionX", 1);
                animator.SetFloat("positionY", 0);
            }
            //Facing left
            if (playerMovement.x < 0)
            {
                dir = Direction.LEFT;
                animator.SetFloat("positionX", -1);
                animator.SetFloat("positionY", 0);
            }
            //Facing down
            if (playerMovement.y < 0)
            {
                dir = Direction.DOWN;
                animator.SetFloat("positionY", -1);
                animator.SetFloat("positionX", 0);
            }
        }

    }

    void OnSword()
    {
        animator.SetTrigger("swordAttack");
    }

    void OnBow()
    {
        animator.SetTrigger("bowAttack");
    }


    public void SwordAttack()
    {
        LockMovement();

        switch (dir)
        {
            case Direction.UP:
                swordAttack.AttackUp();
                break;
            case Direction.DOWN:
                swordAttack.AttackDown();
                break;
            case Direction.LEFT:
                swordAttack.AttackLeft();
                break;
            case Direction.RIGHT:
                swordAttack.AttackRight();
                break;
            default:
                break;
        }
    }

    public void BowAttack()
    {
        bowAttack.Attack();
    }



    public void EndSwordAttack()
    {
        swordAttack.StopAttack();
        UnlockMovement();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void PlantAttack(int damage)
    {
        healed = true;
        if (Health < maxHealth) Health += damage;
        healed = false;
    }

    public void OnMagicAttack()
    {
        if (currentMagic != MAGICe.NOONE)
        {
            if (CanMagic)
            {
                Instantiate(spells, transform.position, Quaternion.identity);
                PersistentManager.Instance.ability.usedAbility(MagicCooldown);
                StartCoroutine(MagicCD());
            }
        }
    }

    private IEnumerator MagicCD()
    {
        CanMagic = false;
        yield return new WaitForSeconds(MagicCooldown);
        CanMagic = true;
    }

    public void OnInteract()
    {
        print("pressing e");
        if (Interacting)
        {
            Ins.Interacted();
            print("entered");
        }
    }

    protected void SetLayer()
    {
        int layer = Mathf.FloorToInt(transform.position.y * 10);
        GetComponent<SpriteRenderer>().sortingOrder = -layer;
    }
}
