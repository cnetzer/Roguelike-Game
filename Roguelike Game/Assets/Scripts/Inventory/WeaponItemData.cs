using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Weapon Item")]
public class WeaponItemData : InventoryItemData
{
    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
