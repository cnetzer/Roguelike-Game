using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventorySystem : ScriptableObject
{
    public List<InventorySlot> inventory = new List<InventorySlot>();

    public void AddItem(InventoryItemData item, int amount)
    {
        var hasItem = false;
        for (var i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item)
            {
                inventory[i].Add(amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            inventory.Add(new InventorySlot(item, amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public InventoryItemData item;
    public int stackSize;

    public InventorySlot(InventoryItemData data, int amount)
    {
        item = data;
        stackSize = amount;
    }

    public void Add(int value)
    {
        stackSize += value;
    }
}