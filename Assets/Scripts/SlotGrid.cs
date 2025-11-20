using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    [SerializeField] private ConfirmBuyUi confirmBuyUi;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject buySlotPrefab;
    private HashSet<Vector2Int> occupiedSlots = new();
    private HashSet<Vector2Int> slotsToBuy = new();

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

    void Awake()
    {
        Vector2Int initialPosition = new(0, 0);
        // Instantiate(slotPrefab, new Vector3(initialPosition.x * 5, 0, initialPosition.y * 5), Quaternion.identity, transform);
        occupiedSlots.Add(initialPosition);
        CreateBuySlotsArround(initialPosition);
    }
}
