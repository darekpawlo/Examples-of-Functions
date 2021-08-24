using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTest : MonoBehaviour
{
    private Singleton_Manager manager;

    private void Awake()
    {
        manager = Singleton_Manager.Instance;
        manager.Test();
    }
}
