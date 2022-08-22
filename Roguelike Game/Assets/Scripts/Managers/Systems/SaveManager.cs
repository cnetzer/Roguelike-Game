using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public InventorySystem inventory;

    private void Awake()
    {
        inventory.Load();
    }

    private void OnApplicationQuit()
    {
        inventory.Save();
        inventory.inventory.items = new InventorySlot[24];
    }
}
