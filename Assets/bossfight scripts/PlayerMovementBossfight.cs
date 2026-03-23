using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBossfight : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    bool busy;
    bool movementInterrupt;
    public bool isParrying;

    [SerializeField] float moveSpeed;

    public bool grounded;

    [SerializeField] GameObject groundCheck;

    [SerializeField] float groundCheckRadius;

    [SerializeField] LayerMask mask;

    [SerializeField] float jumpForce;

    [SerializeField] float maxAirSpeed;

    [SerializeField] float airControl;

    [SerializeField] float attackCastTime;

    [SerializeField] Vector2 knockback;

    [SerializeField] float rollSpeed;

    [SerializeField] int maxHealth;
    public int currentHealth;

    [SerializeField] float rollTime;

    Vector2 movementValue;
    [SerializeField] float parryTime;
    [SerializeField] float hitTime;
    [SerializeField] float jumpTimer;

    public bool rolling;
    bool rollRight;

    bool previousDirection;

    bool jumpReady;
    [SerializeField] GameObject boss;

    public AK.Wwise.Event damageSound;

    private void Awake()
    {
        if(Gamepad.all.Count > 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        jumpReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rolling)
        {
            if (rollRight)
            {
                rb.velocity = new Vector2(rollSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-rollSpeed, rb.velocity.y);
            }
        }
        GroundCheck();
        if (!busy)
        {
            if (grounded)
            {
                if (movementValue.x != 0)
                {
                    anim.Play("run");
                }
                else
                {
                    anim.Play("playerIdle");
                }
            }
            else
            {
                if (rb.velocity.y > 0)
                {
                    anim.Play("jumpUp");
                }
                if (rb.velocity.y < 0)
                {
                    anim.Play("jumpDown");
                }
            }

            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (busy || movementInterrupt)
        {
            return;
        }
        if (grounded)
        {
            rb.velocity = new Vector2(movementValue.x * moveSpeed, rb.velocity.y);

        }
        else if ((rb.velocity.x > maxAirSpeed && movementValue.x > 0) || (rb.velocity.x < -maxAirSpeed && movementValue.x < 0))
        {
            return;
        }
        else
        {
            rb.AddForce(new Vector2(movementValue.x * airControl * 10, 0));
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
        if(movementValue.x > 0)
        {
            previousDirection = true;
        } else if(movementValue.x < 0)
        {
            previousDirection = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (busy) return;
        if (grounded)
        {
            if (context.performed)
            {
                if (!jumpReady) return;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
                GetComponent<PlayerSoundController>().PlayJump();
                jumpReady = false;
                StartCoroutine(JumpCoroutine());
            }
        }
        else
        {
            if (context.canceled)
            {
                rb.gravityScale = 16;
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (busy) return;
            if (grounded)
            {
                rb.velocity = Vector2.zero;
            }
            StartCoroutine(AttackCoroutine());
            anim.Play("playerAttack");
        }
        
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (busy) return;
            if (grounded)
            {
                rb.velocity = Vector2.zero;
            }
            StartCoroutine(ParryCoroutine());
            anim.Play("parry");
        }
    }

    public void OnDodgeRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (busy) return;
            if (!grounded) return;

            StartCoroutine(DodgeRollCoroutine());
            anim.Play("roll");
            //Debug.LogError(movementValue);
            if (previousDirection)
            {
                rollRight = true;
            }
            else
            {
                rollRight = false;
            }
        }
        
    }

    void GroundCheck()
    {
        if (Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, mask))
        {
            grounded = true;
            rb.gravityScale = 4;
        }
        else
        {
            grounded = false;
        }
    }

    void Flip()
    {
        //sr.flipX = (movementValue.x < 0) ? true : false;
        if (movementValue.x < 0)
        {
            sr.flipX = true;
        } else if (movementValue.x > 0)
        {
            sr.flipX = false;
        }
    }

    IEnumerator AttackCoroutine()
    {
        busy = true;
        yield return new WaitForSeconds(attackCastTime);
        busy = false;

    }

    IEnumerator ParryCoroutine()
    {
        busy = true;
        isParrying = true;
        yield return new WaitForSeconds(parryTime);
        busy = false;
        isParrying = false;
    }

    IEnumerator DodgeRollCoroutine()
    {
        busy = true;
        rolling = true;
        //Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(rollTime);
        busy = false;
        rolling = false;
        
        //Physics2D.IgnoreLayerCollision(6, 7, false);
        //transform.position = new Vector3(transform.position.x, -3.6f, transform.position.z);

        //print(transform.position);
    }

    IEnumerator HitCoroutine(bool damaging)
    {
        if (damaging)
        {
            busy = true;
        }
        else
        {
            movementInterrupt = true;
        }
        
        yield return new WaitForSeconds(hitTime);

        if (damaging)
        {
            busy = false;
        }
        else
        {
            movementInterrupt = false;
        }
        
    }

    public void Damage(int data)
    {
        //if (grounded)
        //GetComponent<PlayerSoundController>().PlayDeath();
        damageSound.Post(gameObject);
        rb.velocity = Vector2.zero;
        
        
        if(transform.position.x > boss.transform.position.x)
        {
            rb.AddForce(knockback);

        } else
        {
            rb.AddForce(new Vector2(-knockback.x, knockback.y));
        }

        currentHealth -= data;
        
        if(data > 0)
        {
            StartCoroutine(HitCoroutine(true));
        } else
        {
            StartCoroutine(HitCoroutine(false));
        }
        
        anim.Play("hit");
    }

    IEnumerator JumpCoroutine()
    {
        yield return new WaitForSeconds(jumpTimer);
        jumpReady = true;
    }

    
}
