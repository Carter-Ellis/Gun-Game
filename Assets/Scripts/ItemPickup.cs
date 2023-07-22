using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    Item item;
    Player player;
    Vector3 holdPos = new Vector3(0.312f, -0.087f, -1);
    Shoot shoot;
    
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
        shoot = gameObject.GetComponent<Shoot>();
    }

    void Pickup(GameObject obj)
    {
        item = obj.GetComponent<Item>();
        player.inventory.Add(item);
        Transform child = transform.GetChild(1);
        obj.transform.parent = child.transform;
        Vector3 pos = child.transform.position;
        obj.transform.position = pos + holdPos;
        if (player.equipped)
        {
            obj.SetActive(false);
        }
        player.equipped = true;
       
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Item")
        {           
            Pickup(coll.gameObject);
        }
        
        
    }

}
