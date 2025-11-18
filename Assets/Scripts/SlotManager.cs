using TMPro;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textUi;
    [SerializeField] private GameObject EmptySoil;

    [SerializeField] private GameObject GrowingWheat;
    [SerializeField] private GameObject MatureWheat;

    [SerializeField] private GameObject GrowingBeetroot;
    [SerializeField] private GameObject MatureBeetroot;

    private string plantedType = null;
    private int timeUntilGrown = 0;

    private void UpdateTextUi()
    {
        if (plantedType == null)
        {
            textUi.text = "Plant";
        }
        else if (timeUntilGrown > 0)
        {
            textUi.text = timeUntilGrown.ToString();
        }
        else
        {
            textUi.text = "Harvest";
        }
    }

    public void PlantWheat()
    {
        plantedType = "Wheat";
        EmptySoil.SetActive(false);
        GrowingWheat.SetActive(true);
        timeUntilGrown = 10;
        UpdateTextUi();
    }

    public void PlantBeetroot()
    {
        plantedType = "Beetroot";
        EmptySoil.SetActive(false);
        GrowingBeetroot.SetActive(true);
        timeUntilGrown = 20;
        UpdateTextUi();
    }

    public void PassTime()
    {
        if (plantedType == null) return;
        if (timeUntilGrown <= 0) return;

        timeUntilGrown--;
        UpdateTextUi();
        if (timeUntilGrown == 0)
        {
            if(plantedType == "Wheat")
            {
                GrowingWheat.SetActive(false);
                MatureWheat.SetActive(true);
            }
            else if(plantedType == "Beetroot")
            {
                GrowingBeetroot.SetActive(false);
                MatureBeetroot.SetActive(true);
            }
        }
    }

    public void OpenUi()
    {
        if(plantedType == "Wheat" && timeUntilGrown == 0)
        {
            plantedType = null;
            MatureWheat.SetActive(false);
            EmptySoil.SetActive(true);
            EconomyManager.Instance.AddMoney(2);
            UpdateTextUi();
            return;
        }

        if(plantedType == "Beetroot" && timeUntilGrown == 0)
        {
            plantedType = null;
            EconomyManager.Instance.AddMoney(20);
            MatureBeetroot.SetActive(false);
            EmptySoil.SetActive(true);
            UpdateTextUi();
            return;
        }

        if(plantedType != null) return;
        PlantManager.Instance.OpenMenu(this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PassTime();
        }
    }
}
