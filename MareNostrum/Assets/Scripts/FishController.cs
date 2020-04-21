using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public Transform startingPosition;
    public float timer = 0.5f;
    private float positiveTimer;
    private float negativeTimer;
    public Rigidbody2D rb;
    public float speed;
    private bool direction = true;
    private bool isActive;

    //Cuanta energia aporta al jugador
    public float energyValue;

    // Start is called before the first frame update
    void Start()
    {
        positiveTimer = timer;
        negativeTimer = -timer;
        rb = GetComponent<Rigidbody2D>();
        EnableFish();
        GetComponent<Transform>().position = startingPosition.position;
        //Por si acaso, configuramos siempre que aporte un minimo de energia si no se configura en el inspector.
        if (energyValue <= 0) { energyValue = 10; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableFish()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        isActive = true;
    }

    public void DisableFish()
    {
        rb.velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        isActive = false;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;

            if (timer <= negativeTimer && direction == false)
            {
                rb.velocity = new Vector2  (-speed, 0);
                timer = positiveTimer;
                direction = true;
            }
            else if (timer <= 0.0f && direction == true)
            {
                rb.velocity = new Vector2(+speed, 0);
                direction = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlaySound("Chew1");
            GameManager.Instance.ModifyEnergy(energyValue);
            DisableFish();
        }
    }
}
