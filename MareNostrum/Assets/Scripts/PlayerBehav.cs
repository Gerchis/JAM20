using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehav : MonoBehaviour
{
    public enum Directions
    {
        NONE,
        UP,
        DOWN,
        RIGHT,
        LEFT,
        UP_RIGHT,
        UP_LEFT,
        DOWN_RIGHT,
        DOWN_LEFT
    }

    public float moveForce;
    public float maxVel;

    public float dashForce;
    private bool isDashing;

    private Rigidbody2D rb;

    private Directions facingDirection = Directions.NONE;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
        
    void Update()
    {
        if (InputManager.Instance.dashKey)
        {
            switch (facingDirection)
            {
                case Directions.NONE:
                    break;
                case Directions.UP:
                    rb.AddForce(Vector2.up * dashForce, ForceMode2D.Impulse);
                    break;
                case Directions.DOWN:
                    rb.AddForce(Vector2.down * dashForce, ForceMode2D.Impulse);
                    break;
                case Directions.RIGHT:
                    rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
                    break;
                case Directions.LEFT:
                    rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
                    break;
            }
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            //!Controladores de Inputs
            if (InputManager.Instance.upKey)
            {
                rb.AddForce(Vector2.up * moveForce, ForceMode2D.Force);
                facingDirection = Directions.UP;
            }
            else if (InputManager.Instance.downKey)
            {
                rb.AddForce(Vector2.down * moveForce, ForceMode2D.Force);
                facingDirection = Directions.DOWN;
            }

            if (InputManager.Instance.rightKey && !InputManager.Instance.leftKey)
            {
                rb.AddForce(Vector2.right * moveForce, ForceMode2D.Force);
                facingDirection = Directions.RIGHT;
            }
            else if (InputManager.Instance.leftKey && !InputManager.Instance.rightKey)
            {
                rb.AddForce(Vector2.left * moveForce, ForceMode2D.Force);
                facingDirection = Directions.LEFT;
            }

            //!Ejes diagonales para el dash
            if (InputManager.Instance.rightKey && InputManager.Instance.upKey)
            {
                facingDirection = Directions.UP_RIGHT;
            }
            else if (InputManager.Instance.leftKey && InputManager.Instance.upKey)
            {
                facingDirection = Directions.UP_LEFT;
            }
            else if (InputManager.Instance.rightKey && InputManager.Instance.downKey)
            {
                facingDirection = Directions.DOWN_LEFT;
            }
            else if (InputManager.Instance.leftKey && InputManager.Instance.downKey)
            {
                facingDirection = Directions.DOWN_RIGHT;
            }


            //!Controladores de velocidad
            if (rb.velocity.x > maxVel)
            {
                rb.velocity = new Vector2(maxVel, rb.velocity.y);
            }
            else if (rb.velocity.x < -maxVel)
            {
                rb.velocity = new Vector2(-maxVel, rb.velocity.y);
            }

            if (rb.velocity.y > maxVel)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxVel);
            }
            else if (rb.velocity.y < -maxVel)
            {
                rb.velocity = new Vector2(rb.velocity.x, -maxVel);
            }
        }
        else if (rb.velocity.x < maxVel && rb.velocity.x > -maxVel && rb.velocity.y < maxVel && rb.velocity.y > -maxVel)
        {
            isDashing = false;
        }
    }
}
