using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Slider healthbar;
    Player player;
    void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    void Update()
    {
        healthbar.maxValue = player.maxHealth;
        healthbar.value = player.health;       
        if (Input.GetKeyUp(KeyCode.Q)) 
        {
            player.health -= 10;        
        }
    }
}
