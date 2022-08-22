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
    
    public void AddItem(Item item, int amount)
    {
        if (item.buffs.Length > 0)
        {
            SetEmptySlot(item, amount);
            return;
        }
        
        for (var i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].id == item.Id)
            {
                inventory.items[i].Add(amount);
                return;
            }
        }

        SetEmptySlot(item, amount);
    }
    
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (var i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].id <= -1)
            {
                inventory.items[i].UpdateSlot(_item.Id, _item, _amount);
                return inventory.items[i];
            }
        }
        // what happens if inventory is full
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        var temp = new InventorySlot(item2.id, item2.item, item2.stackSize);
        item2.UpdateSlot(item1.id, item1.item, item1.stackSize);
        item1.UpdateSlot(temp.id, temp.item, temp.stackSize);
    }

    public void RemoveItem(Item _item)
    {
        for (var i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].item == _item)
            {
                inventory.items[i].UpdateSlot(-1, null, 0);
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
            inventory.items[i].UpdateSlot(newInventory.items[i].id, newInventory.items[i].item, newInventory.items[i].stackSize);
        }
        stream.Close();
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        for (var i = 0; i < inventory.items.Length; i++)
        {
            RemoveItem(inventory.items[i].item);
        }
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] items = new InventorySlot[24];
}
[System.Serializable]
public class InventorySlot
{
    public int id = -1;
    public Item item;
    public int stackSize;

    public InventorySlot()
    {
        id = -1;
        item = null;
        stackSize = 0;
    }
    
    public void UpdateSlot(int refId, Item data, int amount)
    {
        id = refId;
        item = data;
        stackSize = amount;
    }
    
    public InventorySlot(int refId, Item data, int amount)
    {
        id = refId;
        item = data;
        stackSize = amount;
    }

    public void Add(int value)
    {
        stackSize += value;
    }
}