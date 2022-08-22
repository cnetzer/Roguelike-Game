using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public InventoryItemData item;
    public void OnBeforeSerialize()
    {
        //GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
    }

    public void OnAfterDeserialize()
    {
        
    }
}
