using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Inventory System/Food Item")]
public class FoodItemData : InventoryItemData
{
    public float restoreHealthValue;

    private void Awake()
    {
        type = ItemType.Food;
    }
}
