using System.Collections;
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
        if (collision.tag == "Player")
        {        
        	SoundManager.Instance.PlaySound("WaterSplash1");
            Vector3 contactVector = transform.position - collision.transform.position;
            Quaternion splashRotation = Quaternion.Euler(0,0,0);

            if (Mathf.Abs(contactVector.y) > Mathf.Abs(contactVector.x) && contactVector.y > 0)
            {
                splashRotation = Quaternion.Euler(0, 0, 0);              
            }
            else if (Mathf.Abs(contactVector.y) > Mathf.Abs(contactVector.x) && contactVector.y < 0)
            {
                splashRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (Mathf.Abs(contactVector.y) < Mathf.Abs(contactVector.x) && contactVector.x > 0)
            {
                splashRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (Mathf.Abs(contactVector.y) < Mathf.Abs(contactVector.x) && contactVector.x < 0)
            {
                splashRotation = Quaternion.Euler(0, 0, 90);
            }

            splash.transform.rotation = splashRotation;
            splash.transform.position = collision.transform.position;


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
        }
    }
}
