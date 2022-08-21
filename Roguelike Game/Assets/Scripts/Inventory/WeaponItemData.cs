using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Weapon Item")]
public class WeaponItemData : InventoryItemData
{
    public int damage = 5;
    
    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
