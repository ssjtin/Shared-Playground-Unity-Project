using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private bool isGettingKnockedBack = false;
    public float thrust;

    // Start is called before the first frame update
    void Start()
    {
        thrust = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isGettingKnockedBack)
        {
            StartCoroutine(StartKnockback(collision.GetComponent<Rigidbody2D>()));
        }
    }

    private IEnumerator StartKnockback(Rigidbody2D hitBody)
    {
        isGettingKnockedBack = true;
        Debug.Log("Player is hit");
        Vector2 difference = hitBody.transform.position - transform.position;
        difference = difference.normalized * thrust;
        Debug.Log(difference);

        for (int i = 0; i < 20; i++)
        {
            hitBody.transform.position = Vector3.MoveTowards(hitBody.transform.position, difference, Time.deltaTime * 2.2f);
        }

        EventHandler.CallOnPlayerHitEvent(4);

        yield return new WaitForSeconds(1f);
        isGettingKnockedBack = false;
    }

}
