using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private EconomyManager economyManager;
    [SerializeField] private GridManager gridManager;
    public PlayerSaveData loadedData = new();

    private float timeSinceLastSave = 0f;
    private const float saveInterval = 60f; // Save every 10 seconds

    public void SaveGame()
    {
        PlayerSaveData data = new()
        {
            money = EconomyManager.Instance.money,
            slotsData = GridManager.Instance.GetSlotsData(),
            lastExitTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        };
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerSaveData", json);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerSaveData"))
        {
            string json = PlayerPrefs.GetString("PlayerSaveData");
            PlayerSaveData playerSaveData = JsonUtility.FromJson<PlayerSaveData>(json);
            economyManager.money = playerSaveData.money;
            gridManager.LoadSlots(playerSaveData.slotsData);
        }
    }

    void Awake()
    {
        LoadGame();
    }

    void Update()
    {
        timeSinceLastSave += Time.deltaTime;
        if (timeSinceLastSave >= saveInterval)
        {
            SaveGame();
            timeSinceLastSave = 0f;
        }
    }
}
