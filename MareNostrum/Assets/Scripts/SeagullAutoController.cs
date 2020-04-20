﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAutoController : MonoBehaviour
{
    public float timer = 0.5f;
    private float positiveTimer;
    private float negativeTimer;
    private Rigidbody2D rb;
    public float speed;  //Valor recomendado 2 en el automatico y 100 en el por puntos
    private bool goingRight = true;
    private bool isActive;

    public Transform[] positions;
    private Vector2 direction;
    private float magnitude1;
    private float magnitude2;
    private int i = 0;
    private bool going = true;
    private float actualDelay;
    public float delayRate;


    public enum SeagullType
    {
        NONE,
        NORMAL,
        AVIATOR,
        ASTRONAUT
    }

    public enum SeagullMovement
    {
        NONE,
        AUTOMATIC,
        POINTS
    }

    public SeagullType seagullType;
    public SeagullMovement seagullMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnableSeagull();

        switch (seagullMovement)
        {
            case SeagullMovement.NONE:
                break;
            case SeagullMovement.AUTOMATIC:
                rb.bodyType = RigidbodyType2D.Dynamic;
                positiveTimer = timer;
                negativeTimer = -timer;
                AutoMove();
                break;
            case SeagullMovement.POINTS:
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            default:
                break;
        }

        switch (seagullType)
        {
            case SeagullType.NONE:
                break;
            case SeagullType.NORMAL:
                //TODO: cargar sprite
                break;
            case SeagullType.AVIATOR:
                //TODO: cargar sprite
                break;
            case SeagullType.ASTRONAUT:
                //TODO: cargar sprite
                break;
            default:
                break;
        }
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
            switch (seagullMovement)
            {
                case SeagullMovement.NONE:
                    break;
                case SeagullMovement.AUTOMATIC:
                    AutoMove();
                    break;
                case SeagullMovement.POINTS:
                    PointMove();
                    break;
                default:
                    break;
            }
        }
    }

    private void AutoMove()
    {
        timer -= Time.deltaTime;

        if (timer <= negativeTimer && goingRight == false)
        {
            Debug.Log("Izquierda");
            rb.velocity = new Vector2(-speed, 0);
            timer = positiveTimer;
            goingRight = true;
        }
        else if (timer <= 0.0f && goingRight == true)
        {
            Debug.Log("Derecha");
            rb.velocity = new Vector2(+speed, 0);
            goingRight = false;
        }
    }

    private void PointMove()
    {
        float deltaSpeed = Time.fixedDeltaTime * speed;
        
        if (going)
        {
            direction = positions[i + 1].position - positions[i].position;
            magnitude1 = direction.magnitude;
            direction = transform.position - positions[i].position;
            magnitude2 = direction.magnitude;

            if (magnitude1 <= magnitude2 && rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
                actualDelay = Time.time + delayRate;
                i++;
            }
            else if (actualDelay < Time.time && rb.velocity == Vector2.zero)
            {
                MovePoint(positions[i].position, positions[i + 1].position, deltaSpeed);
            }

            if (i >= positions.Length - 1)
            {
                going = false;
            }
        }
        else if (!going)
        {
            direction = positions[i - 1].position - positions[i].position;
            magnitude1 = direction.magnitude;
            direction = transform.position - positions[i].position;
            magnitude2 = direction.magnitude;

            if (magnitude1 <= magnitude2 && rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
                actualDelay = Time.time + delayRate;
                i--;
            }
            else if (actualDelay < Time.time && rb.velocity == Vector2.zero)
            {
                MovePoint(positions[i].position, positions[i - 1].position, deltaSpeed);
            }
            if (i <= 0)
            {
                going = true;
            }
        }
        else if (rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MovePoint(Vector2 posA, Vector2 posB, float t)
    {
        direction = posB - posA;
        direction.Normalize();
        rb.velocity = direction * t;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}