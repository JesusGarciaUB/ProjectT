using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum Direction { 
        NONE, UP, DOWN, LEFT, RIGHT
    };
    public enum WEAPON
    {
        SWORD, BOW
    };
    public Direction dir = Direction.NONE;
    public WEAPON weapon;
    public float speed = 1f;
    public float collOffset = 0.02f;
    public ContactFilter2D cF;
    public SwordAttack swordAttack;
    public BowAttack bowAttack;
    Vector2 playerMovement;
    Animator animator;
    Rigidbody2D rb;
    public List<RaycastHit2D> cColl = new List<RaycastHit2D>();
    bool canMove = true;
    public GameObject healthText;
    public GameObject _win_loseScreen;
    // Start is called before the first frame update
    void Start()
    {
        dir = Direction.DOWN;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public int Health
    {
        get { return health; }
        set 
        {
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
            if (health <= 0) Defeated();
        } 
    }

    int health = 10;
    private void Defeated()
    {
        //Set health loss text position on top of the enemy
        GameObject gm = Instantiate(_win_loseScreen);

        //Add damage dealet
        TextMeshProUGUI textMesh = gm.GetComponent<TextMeshProUGUI>();
        textMesh.SetText("You Lose!!");

        _win_loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        _win_loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    private void FixedUpdate()
    {
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
}
