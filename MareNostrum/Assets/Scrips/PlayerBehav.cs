using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehav : MonoBehaviour
{

    private Rigidbody2D rb;
    
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();   
    }
        
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (InputManager.Instance.upKey)
        {
            //rb.AddForce(Vector2.up, ForceMode.Force);
        }
        else if (InputManager.Instance.downKey)
        {

        }

        if (InputManager.Instance.rightKey && !InputManager.Instance.leftKey)
        {

        }
        else if (InputManager.Instance.leftKey && !InputManager.Instance.rightKey)
        {

        }

    }
}
