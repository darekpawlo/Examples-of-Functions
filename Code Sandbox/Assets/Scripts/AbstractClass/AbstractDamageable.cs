using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDamageable : MonoBehaviour, ITakeDamage
{

    [SerializeField] private int maxHealth;

    private int health;

    private  void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
