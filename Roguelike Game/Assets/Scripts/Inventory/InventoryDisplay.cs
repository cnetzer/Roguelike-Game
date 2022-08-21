using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public InventorySystem inventory;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void CreateDisplay()
    {
        for (var i = 0; i < inventory.inventory.Count; i++)
        {
            var obj = Instantiate(inventory.inventory[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventory[i].stackSize.ToString("n0");
        }
    }

    private void UpdateDisplay()
    {
        for (var i = 0; i < inventory.inventory.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.inventory[i]))
            {
                itemsDisplayed[inventory.inventory[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventory.inventory[i].stackSize.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.inventory[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventory[i].stackSize.ToString("n0");
                itemsDisplayed.Add(inventory.inventory[i], obj);
            }
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),Y_START + ((-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN))), 0f);
    }
}
