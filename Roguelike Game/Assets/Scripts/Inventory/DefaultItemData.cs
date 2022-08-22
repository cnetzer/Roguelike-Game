using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New DefaultItem", menuName = "Inventory System/Default Item")]
public class DefaultItemData : InventoryItemData
{
    private void Awake()
    {
        type = ItemType.Default;
    }
}
