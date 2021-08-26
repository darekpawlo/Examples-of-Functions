using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator_Dictionary : MonoBehaviour
{
    [SerializeField] private ResoureType_Dictionary resoureType;

    [SerializeField] private float maxTimer;
    private float timer;

    private void Start()
    {
        timer = maxTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = maxTimer;

            //Add Resource
            ResourceManager_Dictionary.instance.AddResource(resoureType, 1);
        }
    }
}
