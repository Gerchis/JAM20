using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public bool upKey = false;
    public bool downKey = false;
    public bool rightKey = false;
    public bool leftKey = false;

    public bool dashKey = false;

    public bool key1 = false;
    public bool key2 = false;
    public bool key3 = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("Multiple Inputs Managers in scene!!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            downKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            downKey = false;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            upKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            upKey = false;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightKey = false;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftKey = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashKey = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            dashKey = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            key1 = true;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            key1 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            key2 = true;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            key2 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            key3 = true;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            key3 = false;
        }

    }
}
