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

    public enum Environment
    {
        NONE,
        WATER,
        AIR
    }

    public GameManager.CharacterType characterType;

    public float moveForce;
    public float maxVel;

    private Quaternion rotationAngle;
    public float rotationVel;

    public float dashForce;
    private bool isDashing;

    private Rigidbody2D rb;

    public float airGravity;
    public float waterGravity;
    public float fallingGravity;

    public float streamForce;
    public float whirlwindForce;

    public Vector3 centerWater;

    public WaterBlockBehav.WaterDirection waterType = WaterBlockBehav.WaterDirection.NONE;

    private Directions facingDirection = Directions.NONE;

    public Environment environment = Environment.NONE;
    
    private float energyDash;

    private Animator anim;

    void Start()
    {
        if (characterType != GameManager.Instance.characterSelection)
        {
            gameObject.SetActive(false);
        }

        rb = GetComponent<Rigidbody2D>();
        energyDash = GameManager.Instance.energyDash;
        anim = GetComponent<Animator>();
        GameManager.Instance.LoadIngameReferences();
    }
        
    void Update()
    {
        if (GameManager.Instance.playerControl)
        {
            //!Controlador de la direccion
            switch (facingDirection)
            {
                case Directions.NONE:
                    break;
                case Directions.UP:
                    rotationAngle = Quaternion.Euler(0, 0, 0);

                    if (!anim.GetBool("Front"))
                    {
                        anim.SetBool("Front", true);
                        anim.SetBool("Side", false);
                    }
                    break;
                case Directions.DOWN:
                    rotationAngle = Quaternion.Euler(0, 0, 180);

                    if (!anim.GetBool("Front"))
                    {
                        anim.SetBool("Front", true);
                        anim.SetBool("Side", false);
                    }
                    break;
                case Directions.RIGHT:
                    rotationAngle = Quaternion.Euler(0, 0, -90);

                    if (!anim.GetBool("Side"))
                    {
                        anim.SetBool("Side", true);
                        anim.SetBool("Front", false);
                    }
                    break;
                case Directions.LEFT:
                    rotationAngle = Quaternion.Euler(0, 0, 90);

                    if (!anim.GetBool("Side"))
                    {
                        anim.SetBool("Side", true);
                        anim.SetBool("Front", false);
                    }
                    break;
                case Directions.UP_RIGHT:
                    rotationAngle = Quaternion.Euler(0, 0, -45);
                    break;
                case Directions.UP_LEFT:
                    rotationAngle = Quaternion.Euler(0, 0, 45);
                    break;
                case Directions.DOWN_RIGHT:
                    rotationAngle = Quaternion.Euler(0, 0, -135);
                    break;
                case Directions.DOWN_LEFT:
                    rotationAngle = Quaternion.Euler(0, 0, 135);
                    break;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, rotationVel * Time.deltaTime);

            //!Logica del dash
            if (InputManager.Instance.dashKey && !isDashing)
            {
                SoundManager.Instance.PlayRandomSound("WaterDash");
                GameManager.Instance.ModifyEnergy(energyDash);

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
                    case Directions.UP_RIGHT:
                        rb.AddForce((Vector2.up + Vector2.right) * dashForce, ForceMode2D.Impulse);
                        break;
                    case Directions.UP_LEFT:
                        rb.AddForce((Vector2.up + Vector2.left) * dashForce, ForceMode2D.Impulse);
                        break;
                    case Directions.DOWN_RIGHT:
                        rb.AddForce((Vector2.down + Vector2.right) * dashForce, ForceMode2D.Impulse);
                        break;
                    case Directions.DOWN_LEFT:
                        rb.AddForce((Vector2.down + Vector2.left) * dashForce, ForceMode2D.Impulse);
                        break;
                }
                isDashing = true;

                if (!anim.GetBool("Dash"))
                {
                    anim.SetBool("Dash", true);
                }

                InputManager.Instance.dashKey = false;
            } 
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.playerControl)
        {
            //!Control de entorno
            switch (environment)
            {
                case Environment.NONE:
                    break;
                case Environment.WATER:
                    rb.AddForce(Vector2.down * waterGravity, ForceMode2D.Force);

                    switch (waterType)
                    {
                        case WaterBlockBehav.WaterDirection.NONE:
                            break;
                        case WaterBlockBehav.WaterDirection.UP:
                            rb.AddForce(Vector2.up * streamForce, ForceMode2D.Impulse);
                            break;
                        case WaterBlockBehav.WaterDirection.DOWN:
                            rb.AddForce(Vector2.down * streamForce, ForceMode2D.Impulse);
                            break;
                        case WaterBlockBehav.WaterDirection.RIGHT:
                            rb.AddForce(Vector2.right * streamForce, ForceMode2D.Impulse);
                            break;
                        case WaterBlockBehav.WaterDirection.LEFT:
                            rb.AddForce(Vector2.left * streamForce, ForceMode2D.Impulse);
                            break;
                        case WaterBlockBehav.WaterDirection.WHIRLWIND:
                            Vector3 whirlwindDirection = (centerWater - transform.position).normalized;
                            Vector3 whirlwindRotation = new Vector3(-whirlwindDirection.y, whirlwindDirection.x, 0);
                            rb.AddForce((whirlwindDirection + whirlwindRotation) * whirlwindForce, ForceMode2D.Force);
                            break;
                    }
                    break;
                case Environment.AIR:
                    rb.AddForce(Vector2.down * airGravity, ForceMode2D.Force);

                    if (isDashing)
                    {
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
                    }
                    break;
            }

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

                if (!InputManager.Instance.upKey && !InputManager.Instance.downKey && !InputManager.Instance.rightKey && !InputManager.Instance.leftKey)
                {
                    anim.SetBool("Idle", true);
                }
                else
                {
                    anim.SetBool("Idle", false);
                }

                //!Ejes diagonales para el dash
                if (InputManager.Instance.rightKey && InputManager.Instance.upKey && facingDirection != Directions.UP_RIGHT)
                {
                    facingDirection = Directions.UP_RIGHT;
                }
                else if (InputManager.Instance.leftKey && InputManager.Instance.upKey && facingDirection != Directions.UP_LEFT)
                {
                    facingDirection = Directions.UP_LEFT;
                }
                else if (InputManager.Instance.rightKey && InputManager.Instance.downKey && facingDirection != Directions.DOWN_RIGHT)
                {
                    facingDirection = Directions.DOWN_RIGHT;
                }
                else if (InputManager.Instance.leftKey && InputManager.Instance.downKey && facingDirection != Directions.DOWN_LEFT)
                {
                    facingDirection = Directions.DOWN_LEFT;
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
            else if (rb.velocity.x < maxVel && rb.velocity.x > -maxVel && rb.velocity.y < maxVel && rb.velocity.y > -maxVel && environment == Environment.WATER)
            {
                isDashing = false;
                anim.SetBool("Dash", false);
            }
        }
        else
        {
            rb.AddForce(Vector2.down * fallingGravity, ForceMode2D.Force);
        }
    }
}
