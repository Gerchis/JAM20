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
        LEFT
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
        if (CheckBorders())
        {
            //Cambiar el estado del player a agua
        }
    }

}
