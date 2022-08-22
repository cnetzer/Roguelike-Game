using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    public InventorySystem inventory;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var item = col.GetComponent<GroundItem>();
        if (!item) return;
        
        inventory.AddItem(new Item(item.item), 1);
        Destroy(col.gameObject);
    }
}
