using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float timer = 2.0f;
    public float positiveTimer;
    private float negativeTimer;
    public Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        positiveTimer = timer;
        negativeTimer = -timer;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<FishController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            rb.AddForce(transform.right*speed);
        }
        else if (timer <= negativeTimer)
        {
            rb.AddForce(transform.right * -speed);
            timer = positiveTimer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<FishController>().enabled = false;
        }
    }
}
