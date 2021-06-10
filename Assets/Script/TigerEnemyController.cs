using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerEnemyController : MonoBehaviour
{
    private bool isAttacking = false;

    // ========= MOVEMENT =================
    public float speed = 8;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    Rigidbody2D rigidbody2d;

    Vector2 currentInput;

    SpriteRenderer sr;

    // ==== ANIMATION =====
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    // Start is called before the first frame update
    void Start()
    {
        chaseRadius = 5f;
        attackRadius = 1.3f;

        target = GameObject.FindGameObjectWithTag("Player").transform;
        // =========== MOVEMENT ==============
        rigidbody2d = GetComponent<Rigidbody2D>();

        // ==== ANIMATION =====
        animator = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
    }

    void CheckDistance()
    {
        if (isAttacking)
        {
            return;
        }
        else if (Vector3.Distance(target.position, transform.position) < attackRadius)
        {
            animator.SetBool("isWalking", false);
            StartCoroutine(Attack());
        }
        else if (Vector3.Distance(target.position, transform.position) < chaseRadius)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.6f * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float X = target.transform.position.x - transform.position.x;
        float Y = target.transform.position.y - transform.position.y;

        animator.SetFloat("LookX", X);
        animator.SetFloat("LookY", Y);

        CheckDistance();
        //if (isAttacking)
        //{
        //    return;
        //}

        //// ============== MOVEMENT ======================
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //Vector2 move = new Vector2(horizontal, vertical);

        //if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        //{
        //    lookDirection.Set(move.x, move.y);
        //    lookDirection.Normalize();
        //}

        //currentInput = move;

        //// ============== ANIMATION =======================

        //animator.SetFloat("LookX", lookDirection.x);
        //animator.SetFloat("LookY", lookDirection.y);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("isWalking", false);
        //    StartCoroutine(Attack());
        //} 
        //else if (horizontal == 0f && vertical == 0f)
        //{
        //    animator.SetBool("isWalking", false);
        //}
        //else
        //{
        //    animator.SetBool("isWalking", true);
        //}


    }

    void FixedUpdate()
    {
        //Vector2 position = rigidbody2d.position;

        //position = position + currentInput * speed * Time.deltaTime;

        //rigidbody2d.MovePosition(position);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }
}
