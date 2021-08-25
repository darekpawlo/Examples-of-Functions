using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorAttributes : MonoBehaviour
{

    [Header("Character Stats")]
    [SerializeField] private int maxHealth = 100;

    //Right click on the value in inspector to use it
    [ContextMenuItem("Choose Random DPS", "RandomizeDPS")]
    [SerializeField] [Range(1, 10)] private int damagePerSecond = 3;

    [Header("Character Descrpition")]
    [SerializeField] [TextArea] private string characterName;

    [SerializeField] [Multiline] private string characterDescription;

    private int currentHealth;

    //Calling function from the inspector
    [ContextMenu("TakeDamage")]
    public void TakeDamage()
    {
        currentHealth--;
    }

    private void RandomizeDPS()
    {
        damagePerSecond = Random.Range(1, 10);
    }
}
