using System;
using TMPro;
using UnityEngine;

public enum SlotState
{
    Empty,
    Growing,
    Mature,
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

            case SlotState.Growing:
                textUi.text = $"{Math.Ceiling(secondsLeft)}s";
                break;

            case SlotState.Mature:
                textUi.text = "Harvest";
                break;
        }
    }

    public void Plant(int plantId)
    {
        plantedId = plantId;
        PlantData plantData = plantManager.plantsData[plantId];

        icon.sprite = plantData.growingSprite;
        secondsLeft = plantData.growthTime;
        slotState = SlotState.Growing;
        UpdateTextUi();
    }

    public void PassTime(float seconds)
    {
        if (slotState == SlotState.Empty) return;
        if (secondsLeft <= 0) return;

        secondsLeft -= seconds;

        if (secondsLeft <= 0)
        {
            PlantData plantData = plantManager.plantsData[plantedId];
            icon.sprite = plantData.matureSprite;
            slotState = SlotState.Mature;
        }

        UpdateTextUi();
    }

    public void OnClicked()
    {
        if(slotState == SlotState.Mature)
        {
            PlantData plantData = plantManager.plantsData[plantedId];
            EconomyManager.Instance.AddMoney(plantData.sellPrice);
            plantedId = -1;
            slotState = SlotState.Empty;
            icon.sprite = EmptySoil;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PassTime(1f);
        }
    }
}
