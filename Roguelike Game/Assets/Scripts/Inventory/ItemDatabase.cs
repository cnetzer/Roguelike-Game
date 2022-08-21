using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Item Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public InventoryItemData[] items;
    public Dictionary<InventoryItemData, int> GetId = new Dictionary<InventoryItemData, int>();
    public Dictionary<int, InventoryItemData> GetItem = new Dictionary<int, InventoryItemData>();
    
    public void OnBeforeSerialize()
    {
       
    }

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<InventoryItemData, int>();
        GetItem = new Dictionary<int, InventoryItemData>();
        for (var i = 0; i < items.Length; i++)
        {
            GetId.Add(items[i], i);
            GetItem.Add(i, items[i]);
        }
    }
    
    
}
