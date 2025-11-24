using UnityEngine;

public class BuySlot : MonoBehaviour
{
    public GridManager slotGrid;

    public void BuyNewSlot()
    {
        Vector2Int slotPosition = new((int)transform.position.x / 5, (int)transform.position.y / 5);
        slotGrid.TryToBuySlotAt(slotPosition, gameObject);
    }
}
