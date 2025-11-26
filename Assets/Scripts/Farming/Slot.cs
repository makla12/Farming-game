using System;
using TMPro;
using UnityEngine;

public enum SlotState
{
    Empty,
    Growing,
    Mature,
    Planting,
    Harvesting,
}

public class Slot : MonoBehaviour
{
    [SerializeField] private TMP_Text textUi;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Sprite EmptySoil;
    [NonSerialized] public PlantManager plantManager;

    private SlotState slotState = SlotState.Empty;
    private int plantedId = -1;
    private double secondsLeft = 0;

    public void LoadData(int plantedId, double secondsLeft, SlotState slotState)
    {
        this.slotState = slotState;
        this.plantedId = plantedId;
        this.secondsLeft = secondsLeft;

        if(slotState == SlotState.Empty)
        {
            icon.sprite = EmptySoil;
            UpdateTextUi();
            return;
        }

        PlantData plantData = plantManager.plantsData[plantedId];
        if (slotState == SlotState.Growing)
        {
            icon.sprite = plantData.growingSprite;
        }
        else if (slotState == SlotState.Mature)
        {
            icon.sprite = plantData.matureSprite;
        }

        UpdateTextUi();
    }

    private void UpdateTextUi()
    {
        switch(slotState)
        {
            case SlotState.Empty:
                textUi.text = "Plant";
                break;

            case SlotState.Mature:
                textUi.text = "Harvest";
                break;
            
            default:
                textUi.text = $"{Math.Ceiling(secondsLeft)}s";
                break;
        }
    }

    public void Plant(int plantId)
    {
        plantedId = plantId;
        PlantData plantData = plantManager.plantsData[plantId];

        // icon.sprite = plantData.growingSprite;
        secondsLeft = plantData.plantTime;
        slotState = SlotState.Planting;
        UpdateTextUi();
    }

    public void PassTime(double seconds)
    {
        if (slotState == SlotState.Empty) return;
        if(seconds <= 0) return;
        if (secondsLeft <= 0) return;

        double startingTime = secondsLeft;
        secondsLeft -= seconds;

        if (secondsLeft <= 0)
        {
            if(slotState == SlotState.Planting)
            {
                PlantData plantData = plantManager.plantsData[plantedId];
                icon.sprite = plantData.growingSprite;
                secondsLeft = plantData.growthTime;
                slotState = SlotState.Growing;
            }

            else if(slotState == SlotState.Growing)
            {
                PlantData plantData = plantManager.plantsData[plantedId];
                icon.sprite = plantData.matureSprite;
                slotState = SlotState.Mature;
            }

            else if(slotState == SlotState.Harvesting)
            {
                PlantData plantData = plantManager.plantsData[plantedId];
                EconomyManager.Instance.AddMoney(plantData.sellPrice);
                plantedId = -1;
                icon.sprite = EmptySoil;
                slotState = SlotState.Empty;
            }

            PassTime(seconds - startingTime);
        }

        UpdateTextUi();
    }

    public void OnClicked()
    {
        if(slotState == SlotState.Mature)
        {
            PlantData plantData = plantManager.plantsData[plantedId];
            icon.sprite = EmptySoil;
            secondsLeft = plantData.harvestTime;
            slotState = SlotState.Harvesting;
            UpdateTextUi();
            return;
        }

        if(slotState != SlotState.Empty) return;
        plantManager.OpenMenu(this);
    }

    public SlotSaveData GetSlotSaveData()
    {
        SlotSaveData data = new()
        {
            position = new Vector2Int((int)(transform.position.x / 5), (int)(transform.position.y / 5)),
            slotState = slotState,
            plantedId = plantedId,
            secondsLeft = secondsLeft
        };
        return data;
    }

    void Update()
    {
        if(secondsLeft > 0) 
        {
            PassTime(Time.deltaTime);
        }
    }
}
