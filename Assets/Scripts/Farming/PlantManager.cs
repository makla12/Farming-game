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

    public void Plant(int plantId)
    {
        if (selectedSlot == null) return;
        PlantData plantData = plantsData[plantId];
        if (EconomyManager.Instance.SpendMoney(plantData.plantPrice))
        {
            selectedSlot.Plant(plantId);
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
