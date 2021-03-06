﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlockBehav : MonoBehaviour
{
    public enum WaterDirection
    {
        NONE,
        UP,
        DOWN,
        RIGHT,
        LEFT,
        WHIRLWIND
    }

    public WaterDirection direction = WaterDirection.NONE;

    public BorderTriggerBehav[] borders;

    public GameObject splash;

    public float whirlwindExtraForce;

    private Animator anim;
    public Animator animSplash;

    private void Start()
    {
        anim = GetComponent<Animator>();

        switch (direction)
        {
            case WaterDirection.NONE:
                anim.SetBool("Stream", false);
                anim.SetBool("Whirlwind", false);
                break;
            case WaterDirection.UP:
                anim.SetBool("Stream", true);
                break;
            case WaterDirection.DOWN:
                GetComponentInParent<Transform>().Rotate(0, 0, 180);
                anim.SetBool("Stream", true);
                break;
            case WaterDirection.RIGHT:
                GetComponentInParent<Transform>().Rotate(0, 0, -90);
                anim.SetBool("Stream", true);
                break;
            case WaterDirection.LEFT:
                GetComponentInParent<Transform>().Rotate(0, 0, 90);
                anim.SetBool("Stream", true);
                break;
            case WaterDirection.WHIRLWIND:
                anim.SetBool("Whirlwind", true);
                break;
        }
    }

    private bool CheckBorders()
    {
        for (int i = 0; i < borders.Length; i++)
        {
            if (borders[i].col)
            {
                return false;
            }
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gameObject.tag != "Border")
        {        
        	SoundManager.Instance.PlaySound("WaterSplash1");
            Vector3 contactVector = transform.position - collision.transform.position;
            Quaternion splashRotation = Quaternion.Euler(0,0,0);
            Vector3 splashPosition = new Vector3(0,0,0);
            Collider2D col = GetComponent<Collider2D>();

            if (Mathf.Abs(contactVector.y) > Mathf.Abs(contactVector.x) && contactVector.y > 0)
            {
                splashRotation = Quaternion.Euler(0, 0, 0);

                splashPosition = new Vector3(collision.transform.position.x, transform.position.y - col.bounds.extents.y, 0);
            }
            else if (Mathf.Abs(contactVector.y) > Mathf.Abs(contactVector.x) && contactVector.y < 0)
            {
                splashRotation = Quaternion.Euler(0, 0, 180);

                splashPosition = new Vector3(collision.transform.position.x, transform.position.y + col.bounds.extents.y, 0);
            }
            else if (Mathf.Abs(contactVector.y) < Mathf.Abs(contactVector.x) && contactVector.x > 0)
            {
                splashRotation = Quaternion.Euler(0, 0, -90);

                splashPosition = new Vector3(transform.position.x - col.bounds.extents.x, collision.transform.position.y, 0);
            }
            else if (Mathf.Abs(contactVector.y) < Mathf.Abs(contactVector.x) && contactVector.x < 0)
            {
                splashRotation = Quaternion.Euler(0, 0, 90);

                splashPosition = new Vector3(transform.position.x + col.bounds.extents.x, collision.transform.position.y, 0);
            }

            splash.transform.rotation = splashRotation;
            splash.transform.position = splashPosition;

            animSplash.SetTrigger("Splash");

            if (direction == WaterDirection.WHIRLWIND && collision.GetComponent<PlayerBehav>().dashForce == collision.GetComponent<PlayerBehav>().dashBase)
            {
                collision.GetComponent<PlayerBehav>().dashForce += whirlwindExtraForce;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CheckBorders() && collision.tag == "Player")
        {
            PlayerBehav player = collision.GetComponent<PlayerBehav>();

            player.environment = PlayerBehav.Environment.WATER;

            player.waterType = direction;

            player.centerWater = transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerBehav player = collision.GetComponent<PlayerBehav>();

            player.environment = PlayerBehav.Environment.AIR;

            player.waterType = WaterDirection.NONE;

            if (direction == WaterDirection.WHIRLWIND && player.dashForce == player.dashBase + whirlwindExtraForce)
            {
                player.dashForce -= whirlwindExtraForce;
            }
        }
    }
}
