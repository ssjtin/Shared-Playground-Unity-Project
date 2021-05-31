using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonobehaviour<PlayerController>
{
    // ======= STATS ======== //
    [SerializeField]
    private int playerHealth = 10;

    // ========= MOVEMENT =================
    public float speed = 4;

    Rigidbody2D rigidbody2d;

    Vector2 currentInput;

    SpriteRenderer sr;

    // ==== ANIMATION =====
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject damagePopup;

    // Start is called before the first frame update
    void Start()
    {
    
        // =========== MOVEMENT ==============
        rigidbody2d = GetComponent<Rigidbody2D>();

        // ==== ANIMATION =====
        animator = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EventHandler.OnPlayerHitEvent += HandlePlayerHit;
    }

    private void OnDisable()
    {
        EventHandler.OnPlayerHitEvent -= HandlePlayerHit;
    }

    private void HandlePlayerHit(int damage)
    {
        //  Set player sprite to random color
       sr.color = new Color(
    Random.Range(0f, 1f), //Red
    Random.Range(0f, 1f), //Green
    Random.Range(0f, 1f), //Blue
    1);

        //  Decrease player health by damage amount
        playerHealth -= damage;
        Debug.Log($"Remaining health = {playerHealth}");

        //  Show damage popup
        DamagePopup.Create(transform.position + new Vector3(0, 0.5f), damage);

    }

    // Update is called once per frame
    void Update()
    {

        // ============== MOVEMENT ======================
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        currentInput = move;

        // ============== ANIMATION =======================

        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        position = position + currentInput * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

}
