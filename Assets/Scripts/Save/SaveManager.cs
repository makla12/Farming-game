using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private EconomyManager economyManager;
    [SerializeField] private GridManager gridManager;
    public PlayerSaveData loadedData = new();

    private float timeSinceLastSave = 0f;
    private const float saveInterval = 30f;

    public void SaveGame()
    {
        PlayerSaveData data = new()
        {
            money = EconomyManager.Instance.money,
            slotsData = gridManager.GetSlotsData(),
            lastExitTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
        };
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerSaveData", json);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        PlayerSaveData playerSaveData = new();
        if (PlayerPrefs.HasKey("PlayerSaveData"))
        {
            string json = PlayerPrefs.GetString("PlayerSaveData");
            playerSaveData = JsonUtility.FromJson<PlayerSaveData>(json);
        }

        economyManager.money = playerSaveData.money;
        gridManager.LoadSlots(playerSaveData.slotsData);
        PlantManager.PassTime(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - playerSaveData.lastExitTime);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("PlayerSaveData");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    void OnApplicationQuit()
    {
        SaveGame();
    }
}
