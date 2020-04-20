using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public Rigidbody2D rbp;
    public Collider2D a;
    private float playerX;
    private float playerY;
    private float cameraX;
    private float cameraY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbp = player.GetComponent<Rigidbody2D>();
        playerX = player.GetComponent<Transform>().position.x;
        playerY = player.GetComponent<Transform>().position.y;
        cameraX = GetComponent<Transform>().position.x;
        cameraY = GetComponent<Transform>().position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (playerX > cameraX+2)
        {         
            rb.velocity = rbp.velocity;
        }
        if (playerX > cameraX - 2)
        {
            rb.velocity = rbp.velocity;
        }
        if (playerY > cameraY + 1)
        {
            rb.velocity = rbp.velocity;
        }
        if (playerY > cameraY - 3.5)
        {
            rb.velocity = rbp.velocity;
        }
        else
        {
            rb.velocity = rbp.velocity;
        }
    }

}
