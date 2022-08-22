using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Weapon Item")]
public class WeaponItemData : InventoryItemData
{
    private void Awake()
    {
        stackable = false;
        type = ItemType.Weapon;
    }
}
