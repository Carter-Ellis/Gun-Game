using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    Item item;
    Player player;
    Grab grab;
    Vector3 holdPos = new Vector3(0.312f, -0.087f, -6) / 2;
    Shoot shoot;
    public BoxCollider2D boxCollider;
    public GameObject gunObj;
    PlayerController playerController;

    void Awake()
    {
        player = gameObject.GetComponent<Player>();
        shoot = gameObject.GetComponent<Shoot>();
        playerController = gameObject.GetComponent<PlayerController>();
        grab = gameObject.GetComponent<Grab>();
    }

    void Pickup(GameObject obj)
    {
        item = obj.GetComponent<Item>();
        gunObj = obj;
        boxCollider = obj.GetComponent<BoxCollider2D>();
        player.inventory.Add(item);
        Transform child = transform.GetChild(1);
        obj.transform.parent = child.transform;
        Vector3 pos = child.transform.position;
        if (!playerController.facingRight)
        {
            obj.transform.position = pos + -holdPos;
        }
        else
        {
            obj.transform.position = pos + holdPos;
        }
        
        obj.GetComponent<Collider2D>().enabled = false;
        
        if (player.equipped || grab.holding)
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
