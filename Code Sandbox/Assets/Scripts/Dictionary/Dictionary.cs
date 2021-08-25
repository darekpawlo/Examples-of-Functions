using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    Dictionary<string, int> inventory = new Dictionary<string, int>()
    {
        {"Sword", 100},
        {"Magic wand", 1000},
        {"Potion",200},
        {"Shield",1000}
    };

    private void Start()
    {
        Debug.Log($"cost of Sword: {inventory["Sword"]} ");

        inventory["Potion"] = 500;
        Debug.Log($"cost of Potion: {inventory["Potion"]} ");

        //Adds the value to dictionary
        inventory.Add("Knife", 50);
        Debug.Log($"cost of Knife: {inventory["Knife"]} ");

        //Adds the value to dictionary if it doesn't exist already
        inventory["Bracelet"] = 2000;
        Debug.Log($"cost of Bracelet: {inventory["Bracelet"]} ");

        if (inventory.ContainsKey("Shield"))
        {
            inventory["Shield"] = 1000;
            Debug.Log($"cost of Shield: {inventory["Shield"]} ");
        }
        else
        {
            Debug.Log("can't find the key");
        }

        inventory.Remove("Sword");
        if (inventory.ContainsKey("Sword"))
        {
            Debug.Log($"cost of Sword: {inventory["Sword"]} ");
        }
        else
        {
            Debug.Log("removed sword");
        }
    }
}
