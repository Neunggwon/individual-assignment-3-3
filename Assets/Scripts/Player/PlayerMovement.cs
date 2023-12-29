using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float speed;
    private float speedMultiplier;
    bool btnPressed;

    [Header("Jump")]
    public float jumpPower;
    public LayerMask ground;

    [Header("#Info")]
    public Rigidbody2D rigid;
    public Animator animator;
    Vector2 vecMove;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        //GroundCheck();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultilier();
        float targetSpeed = speed * speedMultiplier;
        rigid.velocity = new Vector2(vecMove.x * targetSpeed, rigid.velocity.y);

        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
    }

    public bool GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 1, ground))
        {
            //Debug.Log(GroundCheck());
            return true;
        }
        else
        {
            //Debug.Log(GroundCheck());
            return false;
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log(context.phase);
        if (context.started && GroundCheck())
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        vecMove = context.ReadValue<Vector2>();
        Flip();
        if (context.started)
        {
            animator.SetBool("IsRun", true);
            btnPressed = true;
        }
        else if(context.performed)
        {
        }
        else if (context.canceled)
        {
            animator.SetBool("IsRun", false);
            btnPressed = false;
        }
    }

    private void UpdateSpeedMultilier()
    {
        if (btnPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {
            speedMultiplier -= Time.deltaTime;
            if (speedMultiplier < 0)
            {
                speedMultiplier = 0;
            }
        }
    }



    private void Flip()
    {
        if (vecMove.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (vecMove.x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


}
