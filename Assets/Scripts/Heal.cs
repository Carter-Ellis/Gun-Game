using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{

    public float healAmount = 10f;
    Player player;

    void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            player.health += healAmount;
            if (player.health > player.maxHealth)
            {
                player.health = player.maxHealth;
            }
            Destroy(gameObject);
        }
    }

}
