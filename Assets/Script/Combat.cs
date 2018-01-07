using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour {
    [SerializeField]
    private GameObject gameOverMenu;
    public const int maxHealth = 100;
    [SyncVar]
    public int health = maxHealth;
    [SyncVar]
    public string playerName= "PlayerName";
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
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
}
