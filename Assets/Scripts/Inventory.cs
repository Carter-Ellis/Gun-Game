using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isActive = false;
    GameObject inventory;

    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        inventory.SetActive(isActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isActive = !isActive;
            inventory.SetActive(isActive);

        }
    }

}
