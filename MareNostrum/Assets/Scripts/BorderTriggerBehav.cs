using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTriggerBehav : MonoBehaviour
{
    public bool col = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            col = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            col = false;
        }
    }
}
