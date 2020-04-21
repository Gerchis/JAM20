using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    //private Rigidbody2D rb;
    //private Rigidbody2D rbp;
    //private float playerX;
    //private float playerY;
    //private float cameraX;
    //private float cameraY;

    public float maxY;
    public float maxX;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    rbp = player.GetComponent<Rigidbody2D>();
    //    playerX = player.GetComponent<Transform>().position.x;
    //    playerY = player.GetComponent<Transform>().position.y;
    //    cameraX = GetComponent<Transform>().position.x;
    //    cameraY = GetComponent<Transform>().position.y;
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, 0);
        }
        else if (player.transform.position.y < transform.position.y - maxY)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + maxY, 0);
        }

        if (player.transform.position.x > transform.position.x + maxX)
        {
            transform.position = new Vector3(player.transform.position.x - maxX, transform.position.y, 0);
        }
        else if (player.transform.position.x < transform.position.x - maxY)
        {
            transform.position = new Vector3(player.transform.position.x + maxX, transform.position.y, 0);
        }
    }

    //private void FixedUpdate()
    //{
    //    if (playerX > (cameraX + 2) && rbp.velocity.x > 0)
    //    {         
    //        rb.velocity = rbp.velocity;
    //    }
    //    else if (playerX < (cameraX - 2) && rbp.velocity.x < 0)
    //    {
    //        rb.velocity = rbp.velocity;
    //    }
    //    else if (playerY > (cameraY + 1) && rbp.velocity.y > 0)
    //    {
    //        rb.velocity = rbp.velocity;
    //    }
    //    else if (playerY < (cameraY - 3.5) && rbp.velocity.y < 0)
    //    {
    //        rb.velocity = rbp.velocity;
    //    }
    //    else
    //    {
    //        rb.velocity = new Vector2 (0,0);
    //    }
    //}

}
