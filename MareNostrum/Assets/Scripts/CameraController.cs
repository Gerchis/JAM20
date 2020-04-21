using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float maxY;
    public float maxX;
    public float edgeX;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        if (player.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
        else if (player.transform.position.y < transform.position.y - maxY)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + maxY, transform.position.z);
        }

        if (player.transform.position.x > transform.position.x + maxX)
        {
            transform.position = new Vector3(player.transform.position.x - maxX, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x < transform.position.x - maxX)
        {
            transform.position = new Vector3(player.transform.position.x + maxX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > edgeX)
        {
            transform.position = new Vector3(edgeX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -edgeX)
        {
            transform.position = new Vector3(-edgeX, transform.position.y, transform.position.z);
        }

    }
}
