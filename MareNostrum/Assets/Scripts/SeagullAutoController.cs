using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAutoController : MonoBehaviour
{
    public float timer = 0.5f;
    private float positiveTimer;
    private float negativeTimer;
    public Rigidbody2D rb;
    public float speed;
    private bool direction = true;
    private bool isActive;

    public enum SEAGULLTYPE
    {
        NONE,

    }
    

    // Start is called before the first frame update
    void Start()
    {
        positiveTimer = timer;
        negativeTimer = -timer;
        rb = GetComponent<Rigidbody2D>();
        EnableSeagull();
    }

    public void EnableSeagull()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;

            if (timer <= negativeTimer && direction == false)
            {
                Debug.Log("Izquierda");
                rb.velocity = new Vector2(-speed, 0);
                timer = positiveTimer;
                direction = true;
            }
            else if (timer <= 0.0f && direction == true)
            {
                Debug.Log("Derecha");
                rb.velocity = new Vector2(+speed, 0);
                direction = false;
            }
        }
    }
}
