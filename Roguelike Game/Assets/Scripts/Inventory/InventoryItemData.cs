using UnityEngine;

public enum ItemType
{
    Default,
    Food,
    Weapon,
    Equipment
}

public abstract class InventoryItemData : ScriptableObject
{
    public int id;
    public string displayName;
    [TextArea(15, 20)] public string description;
    public Sprite icon;
    public ItemType type;
    public GameObject prefab;
}
