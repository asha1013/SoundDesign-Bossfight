using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    [SerializeField]
    float moveSpeed;

    public bool grounded;

    [SerializeField]
    GameObject groundCheck;

    [SerializeField]
    float groundCheckRadius;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float maxAirSpeed;

    [SerializeField]
    float airControl;

    Vector2 movementValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        
    }

    private void FixedUpdate()
    {
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

        if (context.performed)
        {
            Flip();
        }
        
        //Debug.Log(movementValue);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
            }
        } else
        {
            if (context.canceled)
            {
                rb.gravityScale = 16;
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
        sr.flipX = (movementValue.x < 0) ?  true : false;
    }
}
