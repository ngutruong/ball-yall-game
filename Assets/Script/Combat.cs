using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    public const int maxHealth = 100;
    public int health = maxHealth;
    public string playerName= "Vito";

    public bool TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Dead!");
            Destroy(gameObject);
            return true;
        }
        return false;
    }
    public void AddHealth()
    {
        if (health <= 100)
        {
            health += 10;
            Debug.Log("Dead!");
        }
    }
}
