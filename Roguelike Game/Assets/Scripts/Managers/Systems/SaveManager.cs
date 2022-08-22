using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public InventorySystem inventory;
    public InventorySystem equipment;

    private void Awake()
    {
        inventory.Load();
        equipment.Load();
    }

    private void OnApplicationQuit()
    {
        inventory.Save();
        inventory.inventory.Clear();
        
        equipment.Save();
        equipment.inventory.Clear();
    }
}
