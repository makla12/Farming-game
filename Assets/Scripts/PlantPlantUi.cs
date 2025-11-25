using TMPro;
using UnityEngine;

public class PlantPlantUi : MonoBehaviour
{
    private PlantManager plantManager;
    private int plantId = -1;
    [SerializeField] private TMP_Text plantTypeText;
    [SerializeField] private TMP_Text plantPriceText;

    public void Setup(PlantManager plantManager, int plantId)
    {
        this.plantManager = plantManager;
        this.plantId = plantId;
        PlantData plantData = plantManager.plantsData[plantId];
        plantTypeText.text = plantData.plantType;
        plantPriceText.text = $"Cost: {plantData.plantPrice}";
    }

    public void Plant()
    {
        if(plantId == -1) return;

        plantManager.Plant(plantId);
    }
}
