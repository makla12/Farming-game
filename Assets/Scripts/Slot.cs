using System;
using TMPro;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private TMP_Text textUi;
    [SerializeField] private GameObject EmptySoil;

    [SerializeField] private GameObject GrowingWheat;
    [SerializeField] private GameObject MatureWheat;

    [SerializeField] private GameObject GrowingBeetroot;
    [SerializeField] private GameObject MatureBeetroot;

    private string plantedType = "";
    private double secondsLeft = 0;

    public void LoadData(string plantedType, double secondsLeft)
    {
        this.plantedType = plantedType;
        this.secondsLeft = secondsLeft;

        if(plantedType == "Wheat")
        {
            EmptySoil.SetActive(false);
            if (secondsLeft > 0)
            {
                GrowingWheat.SetActive(true);
            }
            else
            {
                MatureWheat.SetActive(true);
            }
        }
        else if(plantedType == "Beetroot")
        {
            EmptySoil.SetActive(false);
            if (secondsLeft > 0)
            {
                GrowingBeetroot.SetActive(true);
            }
            else
            {
                MatureBeetroot.SetActive(true);
            }
        }

        UpdateTextUi();
    }

    private void UpdateTextUi()
    {
        if (plantedType == "")
        {
            textUi.text = "Plant";
        }
        else if (secondsLeft > 0)
        {
            textUi.text = $"{Math.Ceiling(secondsLeft)}s";
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
        secondsLeft = 10;
        UpdateTextUi();
    }

    public void PlantBeetroot()
    {
        plantedType = "Beetroot";
        EmptySoil.SetActive(false);
        GrowingBeetroot.SetActive(true);
        secondsLeft = 20;
        UpdateTextUi();
    }

    public void PassTime(float seconds)
    {
        if (plantedType == "") return;
        if (secondsLeft <= 0) return;

        secondsLeft -= seconds;
        UpdateTextUi();
        if (secondsLeft <= 0)
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
        if(plantedType == "Wheat" && secondsLeft <= 0)
        {
            plantedType = "";
            MatureWheat.SetActive(false);
            EmptySoil.SetActive(true);
            EconomyManager.Instance.AddMoney(2);
            UpdateTextUi();
            return;
        }

        if(plantedType == "Beetroot" && secondsLeft <= 0)
        {
            plantedType = "";
            EconomyManager.Instance.AddMoney(20);
            MatureBeetroot.SetActive(false);
            EmptySoil.SetActive(true);
            UpdateTextUi();
            return;
        }

        if(plantedType != "") return;
        PlantManager.Instance.OpenMenu(this);
    }

    public SlotSaveData GetSlotSaveData()
    {
        SlotSaveData data = new()
        {
            position = new Vector2Int((int)(transform.position.x / 5), (int)(transform.position.y / 5)),
            plantedType = plantedType,
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
