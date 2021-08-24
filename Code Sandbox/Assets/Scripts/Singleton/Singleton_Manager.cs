using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_Manager : Singleton<Singleton_Manager>
{
    public void Test()
    {
        Debug.Log("Test");
    }
}
