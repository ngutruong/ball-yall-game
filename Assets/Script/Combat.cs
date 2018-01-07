using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {
    [SerializeField]
    private GameObject gameOverMenu;
    public const int maxHealth = 100;
    public int health = maxHealth;
    public string playerName= "Vito";
    void Start()
    {
        playerName = PlayerPrefs.GetString("playerName");
    }
    public bool TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
            gameOverMenu.SetActive(true);
            return true;
        }
        return false;
    }
    public void AddHealth()
    {
        if (health <= 100)
        {
            health += 10;
        }
    }
}
