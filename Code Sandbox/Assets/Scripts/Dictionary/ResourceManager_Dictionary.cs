using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager_Dictionary : MonoBehaviour
{
    public event EventHandler OnResourceAdded;

    public static ResourceManager_Dictionary instance;

    [SerializeField] private List<ResoureType_Dictionary> resourceTypeList = new List<ResoureType_Dictionary>();

    private Dictionary<ResoureType_Dictionary, int> resourceDictionary = new Dictionary<ResoureType_Dictionary, int>();

    private void Awake()
    {
        instance = this;

        //Creates key from the start
        foreach (ResoureType_Dictionary resoureType in resourceTypeList)
        {
            resourceDictionary[resoureType] = 0;
        }
    }

    private void Start()
    {
        OnResourceAdded += ResourceManager_Dictionary_OnResourceAdded;

        //TestDisplayResources();
    }

    public void AddResource(ResoureType_Dictionary resoureType, int val)
    {        
        //Creates key when resource is added
        /*if (!resourceDictionary.ContainsKey(resoureType))
        {
            resourceDictionary[resoureType] = 0;
        }*/

        resourceDictionary[resoureType] += val;

        OnResourceAdded?.Invoke(this, EventArgs.Empty);
    }

    private void ResourceManager_Dictionary_OnResourceAdded(object sender, EventArgs e)
    {
        //TestDisplayResources();
    }

    private void TestDisplayResources()
    {
        foreach (ResoureType_Dictionary resoureType in resourceDictionary.Keys)
        {
            Debug.Log(resoureType.name + " : " + resourceDictionary[resoureType]);
        }
    }

    public int GetResourceAmount(ResoureType_Dictionary resoureType)
    {
        return resourceDictionary[resoureType];
    }
}
