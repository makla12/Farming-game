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
    [NonSerialized] public SlotManager selectedSlot = null;

    public void OpenMenu(SlotManager slot)
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

    public void PassTime()
    {
        SlotManager[] slots = FindObjectsByType<SlotManager>(FindObjectsSortMode.InstanceID);
        foreach (SlotManager slot in slots)
        {
            slot.PassTime(1f);
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
