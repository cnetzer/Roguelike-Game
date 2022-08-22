using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Item Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public InventoryItemData[] items;
    public Dictionary<int, InventoryItemData> GetItem = new Dictionary<int, InventoryItemData>();

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, InventoryItemData>();
    }

    public void OnAfterDeserialize()
    {
        for (var i = 0; i < items.Length; i++)
        {
            items[i].data.Id = i;
            GetItem.Add(i, items[i]);
        }
    }
    
    
}
