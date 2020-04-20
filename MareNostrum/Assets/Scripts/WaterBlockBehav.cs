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
