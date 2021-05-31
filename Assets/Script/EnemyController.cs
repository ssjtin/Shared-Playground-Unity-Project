using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;

    private Transform target;

    private SpriteRenderer sr;

    private Rigidbody2D body;

    //public static event System.Action playerHit;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float range;

    private bool isBouncing = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        if (!isBouncing)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isBouncing)
        {
            EventHandler.CallOnPlayerHitEvent(2);
            isBouncing = true;
            StartCoroutine(BounceOffPlayer());
        }

    }

    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector3(x, y, 0f);
    }

    private IEnumerator BounceOffPlayer()
    {
        var xChange = Random.Range(-5, 5);
        var yChange = Random.Range(-5, 5);

        Vector3 destination = gameObject.transform.position;
        destination.x += xChange;
        destination.y += yChange;

        for (int i = 0; i < 8; i++)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, Time.deltaTime * 20);
        }

        yield return new WaitForSeconds(2);

        isBouncing = false;
        yield return null;
    }
}
