using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public float enemyHp;

    public GameObject player;
    public Rigidbody2D target;

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

        if(sprite.flipX)
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
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y);
        target.velocity = new Vector2(target.velocity.x, target.velocity.y);
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed;
        //Vector2 diraction = rigid.position + nextVec;
        //Vector2 targetdiraction = new Vector2(diraction.x, rigid.position.y);
        float dirVecX = rigid.position.x + nextVec.x;
        Vector2 targetdiraction = new Vector2(dirVec.x, rigid.position.y);
        //rigid.MovePosition(targetdiraction);
        rigid.AddForce(targetdiraction);
    }

    public bool GroundCheck()
    {
        return false;
        //보는 방향에 레이저를 발사 
    }
    private void Jump()
    {
        //레이저가 Ground 있으면 점프를 하도록 구현해보자 .. - PlayerMovement 참조하자..
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
