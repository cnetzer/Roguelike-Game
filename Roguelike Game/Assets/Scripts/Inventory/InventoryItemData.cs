using UnityEngine;

public enum ItemType
{
    Default,
    Food,
    Weapon,
    Equipment
}

public enum Attributes
{
    Strength,
    Durability
}

public abstract class InventoryItemData : ScriptableObject
{
    public int Id;
    public string displayName;
    [TextArea(15, 20)] public string description;
    public ItemType type;
    public Sprite uiDisplay;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        var newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;

    public Item(InventoryItemData item)
    {
        Name = item.name;
        Id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];
        for (var i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}