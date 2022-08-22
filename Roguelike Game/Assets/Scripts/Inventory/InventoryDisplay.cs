using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public InventorySystem inventory;
    public GameObject inventoryPrefab;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    private Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        CreateSlots();
    }

    private void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (var slot in itemsDisplayed)
        {
            if (slot.Value.id >= 0)
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    inventory.database.GetItem[slot.Value.item.Id].uiDisplay;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text =
                    slot.Value.stackSize == 1 ? "" : slot.Value.stackSize.ToString("n0");
            }
            else
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (var i = 0; i < inventory.inventory.items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            
            itemsDisplayed.Add(obj, inventory.inventory.items[i]);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),Y_START + ((-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN))), 0f);
    }
}
