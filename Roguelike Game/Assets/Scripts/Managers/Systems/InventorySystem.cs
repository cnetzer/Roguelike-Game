using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventorySystem : ScriptableObject
{
    public string savePath;
    public ItemDatabase database;
    public Inventory inventory;
    
    public bool AddItem(Item item, int amount)
    {
        if (EmptySlotCount <= 0)
            return false;
        var slot = FindItemOnInventory(item);
        if (!database.GetItem[item.Id].stackable || slot == null)
        {
            SetEmptySlot(item, amount);
            return true;
        }
        slot.Add(amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            var counter = 0;
            for (var i = 0; i < inventory.items.Length; i++)
            {
                if (inventory.items[i].item.Id <= -1)
                    counter++;
            }

            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (var i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].item.Id == _item.Id)
            {
                return inventory.items[i];
            }
        }

        return null;
    }
    
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (var i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].item.Id <= -1)
            {
                inventory.items[i].UpdateSlot(_item, _amount);
                return inventory.items[i];
            }
        }
        // what happens if inventory is full
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (!item2.CanPlaceInSlot(item1.ItemData) || !item1.CanPlaceInSlot(item2.ItemData)) return;
        
        var temp = new InventorySlot(item2.item, item2.stackSize);
        item2.UpdateSlot(item1.item, item1.stackSize);
        item1.UpdateSlot(temp.item, temp.stackSize);
    }
        

    public void RemoveItem(Item _item)
    {
        for (var i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].item == _item)
            {
                inventory.items[i].UpdateSlot(null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        // var saveData = JsonUtility.ToJson(this, true);
        // var bf = new BinaryFormatter();
        // var file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        // bf.Serialize(file, saveData);
        // file.Close();

        var formatter = new BinaryFormatter();
        var stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create,
            FileAccess.Write);
        formatter.Serialize(stream, inventory);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (!File.Exists(string.Concat(Application.persistentDataPath, savePath))) return;
        
        // var bf = new BinaryFormatter();
        // var file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
        // JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        // file.Close();

        var formatter = new BinaryFormatter();
        var stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open,
            FileAccess.Read);
        var newInventory = (Inventory) formatter.Deserialize(stream);
        for (var i = 0; i < inventory.items.Length; i++)
        {
            inventory.items[i].UpdateSlot(newInventory.items[i].item, newInventory.items[i].stackSize);
        }
        stream.Close();
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        for (var i = 0; i < inventory.items.Length; i++)
        {
            inventory.Clear();
        }
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] items = new InventorySlot[24];

    public void Clear()
    {
        for (var i = 0; i < items.Length; i++)
        {
            items[i].UpdateSlot(null, 0);
        }
    }
}
[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    public UserInterface parent;
    public Item item;
    public int stackSize;

    public InventoryItemData ItemData
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.GetItem[item.Id];
            }

            return null;
        }
    }
    
    public InventorySlot()
    {
        item = null;
        stackSize = 0;
    }
    
    public void UpdateSlot(Item data, int amount)
    {
        item = data;
        stackSize = amount;
    }

    public void RemoveItem()
    {
        item = new Item();
        stackSize = 0;
    }
    
    public InventorySlot(Item data, int amount)
    {
        item = data;
        stackSize = amount;
    }

    public void Add(int value)
    {
        stackSize += value;
    }

    public bool CanPlaceInSlot(InventoryItemData _item)
    {
        if (AllowedItems.Length <= 0 || _item == null || _item.data.Id < 0)
            return true;
        for (var i = 0; i < AllowedItems.Length; i++)
        {
            if (_item.type == AllowedItems[i])
                return true;
        }

        return false;
    }
}