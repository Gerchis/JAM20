using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceScreen : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.ApplyResolution();
    }
}
