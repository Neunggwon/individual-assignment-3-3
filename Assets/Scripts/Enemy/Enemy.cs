using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float enemyHp;

    public GameObject player;
    public Rigidbody2D target;

    public LayerMask ground;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    //private void Update()
    //{
    //}

    private void FixedUpdate()
    {
        Move();
        //Jump();

        if (sprite.flipX)
        {
            Debug.DrawRay(rigid.position, Vector3.left, new Color(0, 0, 0));
        }
        else
        {
            Debug.DrawRay(rigid.position, Vector3.right, new Color(0, 0, 0));
        }
    }

    private void LateUpdate()
    {
        Flip();
    }

    private void Move()
    {
        if (sprite.flipX)
        {
            rigid.velocity = new Vector2(Vector2.left.x * speed, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(Vector2.right.x * speed, rigid.velocity.y);
        }
    }

    public bool GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 1, ground))
        {
            //보는 방향에 레이저를 발사 
            if (sprite.flipX)
            {
                if (Physics2D.Raycast(transform.position, Vector3.left, 1, ground))
                {
                    //Debug.Log(GroundCheck());
                    return true;
                }
                return false;
            }
            else
            {
                if (Physics2D.Raycast(transform.position, Vector3.right, 1, ground))
                {
                    //Debug.Log(GroundCheck());
                    return true;
                }
                return false;
            }
        }
        else
        {
            //Debug.Log(GroundCheck());
            return false;
        }
    }
    private void Jump()
    {
        if (GroundCheck())
        {
            Debug.Log("Enemy Jump");
            rigid.velocity = new Vector2(rigid.velocity.x, Vector2.up.y * speed);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Hit());
        }
    }
    public void OnHit()
    {
        Debug.Log("Player Hit!!");
    }


    IEnumerator Hit()
    {
        Debug.Log("Player Hit!!");
        //데미지!!
        yield return new WaitForSeconds(1f);
    }

    private void Flip()
    {
        sprite.flipX = player.transform.position.x < transform.position.x;
    }
}
