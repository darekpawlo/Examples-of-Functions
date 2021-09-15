using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_Test : MonoBehaviour
{
    public Func<int> func;

    private int funcTest;

    private void Start()
    {
        Debug.Log(func());
    }
}
