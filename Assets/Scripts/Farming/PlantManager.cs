using System;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Slot selectedSlot = null;
    [SerializeField] GameObject plantUi;
    public PlantData[] plantsData;

    public void OpenMenu(Slot slot)
    {
        selectedSlot = slot;
        plantUi.SetActive(true);
    }

    public void CloseMenu()
    {
        selectedSlot = null;
        plantUi.SetActive(false);
    }

    public void PlantWheat()
    {
        if (selectedSlot == null) return;
        if (EconomyManager.Instance.SpendMoney(1))
        {
            selectedSlot.Plant(0);
            CloseMenu();
        } 
    }

    public void PlantBeetroot()
    {
        if(selectedSlot == null) return;
        if (EconomyManager.Instance.SpendMoney(15))
        {
            selectedSlot.Plant(1);
            CloseMenu();
        } 
    }

    public static void PassTime(long seconds)
    {
        Slot[] slots = FindObjectsByType<Slot>(FindObjectsSortMode.InstanceID);
        foreach (Slot slot in slots)
        {
            slot.PassTime(seconds);
        }
    }
}
