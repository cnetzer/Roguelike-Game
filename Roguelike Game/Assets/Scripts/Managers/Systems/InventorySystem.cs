using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventorySystem : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabase _database;
    public List<InventorySlot> inventory = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        _database = (ItemDatabase) AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset",
            typeof(ItemDatabase));
#else
        _database = Resources.Load<ItemDatabase>("Database");        
#endif
    }

    public void AddItem(InventoryItemData item, int amount)
    {
        for (var i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item == item)
            {
                inventory[i].Add(amount);
                return;
            }
        }

        inventory.Add(new InventorySlot(_database.GetId[item], item, amount));

    }

    public void Save()
    {
        var saveData = JsonUtility.ToJson(this, true);
        var bf = new BinaryFormatter();
        var file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        for (var i = 0; i < inventory.Count; i++)
            inventory[i].item = _database.GetItem[inventory[i].id];
    }
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public InventoryItemData item;
    public int stackSize;

    public InventorySlot(int refId, InventoryItemData data, int amount)
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