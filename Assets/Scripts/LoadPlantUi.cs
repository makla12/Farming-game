using UnityEngine;

public class LoadPlantUi : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    [SerializeField] GameObject plantPlantUiPrefab;

    void Start()
    {
        for(var i = 0; i < plantManager.plantsData.Length; i++)
        {
            PlantPlantUi plantPlantUi = Instantiate(plantPlantUiPrefab, transform).GetComponent<PlantPlantUi>();
            plantPlantUi.Setup(plantManager, i);
        }
    }
}
