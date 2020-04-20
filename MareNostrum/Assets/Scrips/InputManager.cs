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
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
        else if (Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.DownArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
    }
}
