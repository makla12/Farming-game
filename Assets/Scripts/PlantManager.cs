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

    [SerializeField] GameObject plantUi;
    [NonSerialized] public Slot selectedSlot = null;

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
            selectedSlot.PlantWheat();
            CloseMenu();
        } 
    }

    public void PlantBeetroot()
    {
        if(selectedSlot == null) return;
        if (EconomyManager.Instance.SpendMoney(15))
        {
            selectedSlot.PlantBeetroot();
            CloseMenu();
        } 
    }

    public void PassTime(long seconds)
    {
        Slot[] slots = FindObjectsByType<Slot>(FindObjectsSortMode.InstanceID);
        foreach (Slot slot in slots)
        {
            slot.PassTime(seconds);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlantWheat();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlantBeetroot();
        }
    }
}
