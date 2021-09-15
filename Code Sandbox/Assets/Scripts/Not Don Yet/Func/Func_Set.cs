using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_Set : MonoBehaviour
{
    private Func_Test Func_Test;

    private void Awake()
    {
        Func_Test = GetComponent<Func_Test>();
        Func_Test.func = () => 10;
    }
}
