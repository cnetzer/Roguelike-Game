using System;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject characterInfo;

    private void Start()
    {
        inventory.SetActive(false);
        characterInfo.SetActive(false);
    }

    public void ToggleInventoryUI()
    {
        inventory.SetActive(!inventory.activeSelf);
    }

    public void ToggleCharacterInfo()
    {
        characterInfo.SetActive(!characterInfo.activeSelf);
    }
}
