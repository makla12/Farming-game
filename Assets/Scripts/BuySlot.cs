using UnityEngine;

public class BuySlot : MonoBehaviour
{
    public SlotGrid slotGrid;

    public void BuyNewSlot()
    {
        if (EconomyManager.Instance.SpendMoney(50))
        {
            Vector2Int slotPosition = new((int)transform.position.x / 5, (int)transform.position.z / 5);
            slotGrid.BuySlotAt(slotPosition);
            Destroy(gameObject);
        }
    }
}
