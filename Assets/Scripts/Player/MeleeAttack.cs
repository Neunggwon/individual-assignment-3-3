using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour
{
    public int damage;

    public GameObject weapon;
    public BoxCollider2D collider;
    public Animator animator;

    private void Awake()
    {
        collider = GetComponentInChildren<BoxCollider2D>();
    }
    void Start()
    {
        collider.enabled = false;
    }

    public void MeleeAttacking(InputAction.CallbackContext context)
    {
        //애니메이션을 이용하여 구현 해보자 짜싁아
        if (context.started)
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        animator.SetBool("IsMeleeAttack", true);
        yield return new WaitForSeconds(0.15f);
        collider.enabled = true;
        yield return new WaitForSeconds(0.35f);
        animator.SetBool("IsMeleeAttack", false);
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy"))
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!!");
            //데미지!! health -= damage;

            //디버그가 안뜸..
        }
    }
}
