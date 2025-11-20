using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
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

    [SerializeField] private ConfirmBuyUi confirmBuyUi;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject buySlotPrefab;
    private HashSet<Vector2Int> occupiedSlots = new();
    private HashSet<Vector2Int> slotsToBuy = new();

    public void LoadSlots(List<SlotSaveData> slotsSaveData)
    {
        foreach(var slotData in slotsSaveData)
        {
            occupiedSlots.Add(slotData.position);
            Slot slot = Instantiate(slotPrefab, new Vector3(slotData.position.x * 5, 0, slotData.position.y * 5), Quaternion.identity, transform).GetComponent<Slot>();
            slot.LoadData(slotData.plantedType, slotData.secondsLeft);
        }

        foreach (var pos in occupiedSlots)
        {
            CreateBuySlotsArround(pos);
        }
    }

    public List<SlotSaveData> GetSlotsData()
    {
        List<SlotSaveData> slotsData = new();
        Slot[] slots = FindObjectsByType<Slot>(FindObjectsSortMode.InstanceID);
        foreach (var slot in slots)
        {
            slotsData.Add(slot.GetSlotSaveData());
        }

        return slotsData;
    }

    private void CreateBuySlotsArround(Vector2Int position)
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            new(1, 0),
            new(-1, 0),
            new(0, 1),
            new(0, -1),
            new(1, 1),
            new(1, -1),
            new(-1, 1),
            new(-1, -1)
        };

        foreach (var dir in directions)
        {
            Vector2Int adjacentPos = position + dir;
            if (!occupiedSlots.Contains(adjacentPos) && !slotsToBuy.Contains(adjacentPos))
            {
                Instantiate(buySlotPrefab, new Vector3(adjacentPos.x * 5, 0, adjacentPos.y * 5), Quaternion.identity, transform).GetComponent<BuySlot>().slotGrid = this;
                slotsToBuy.Add(adjacentPos);
            }
        }
    }

    public void TryToBuySlotAt(Vector2Int position, GameObject toDestroy)
    {
        confirmBuyUi.Setup(this, position, slotCost: 50, toDestroy);
    }

    public void BuySlotAt(Vector2Int position)
    {
        if (!occupiedSlots.Contains(position) && slotsToBuy.Contains(position))
        {
            occupiedSlots.Add(position);
            slotsToBuy.Remove(position);
            Instantiate(slotPrefab, new Vector3(position.x * 5, 0, position.y * 5), Quaternion.identity, transform);
            CreateBuySlotsArround(position);
        }
    }
}
