using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    Enemy enemy;
    Player player;
    int damage = 0;
    float Xpos;
    int despawnDist = 10;
    void Awake()
    {
        enemy = GameObject.FindObjectOfType<Enemy>();
        player = GameObject.FindObjectOfType<Player>();
        damage = enemy.damage;
    }

    void Update()
    {
        Xpos = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (Xpos > despawnDist)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Player>() != null)
        {
            player.health -= damage;
            Destroy(gameObject);
        }
        else if (enemy == null || coll.gameObject != enemy.gameObject)
        {
            Destroy(gameObject);
        }
    }

}
