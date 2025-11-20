using UnityEngine;

public class ConfirmBuyUi : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private SlotGrid slotGrid;
    private Vector2Int slotPosition;
    private GameObject toDestroy;
    private int slotCost = 0;

    public void Setup(SlotGrid slotGrid, Vector2Int slotPosition, int slotCost, GameObject toDestroy)
    {
        this.slotGrid = slotGrid;
        this.slotPosition = slotPosition;
        this.slotCost = slotCost;
        this.toDestroy = toDestroy;
        panel.SetActive(true);
    }

    public void ConfirmPurchase()
    {
        if(slotGrid == null) return;

        if(EconomyManager.Instance.SpendMoney(slotCost))
        {
            slotGrid.BuySlotAt(slotPosition);
            Destroy(toDestroy);
            CloseWindow();
        } 
    }

    public void CloseWindow()
    {
        slotGrid = null;
        slotPosition = Vector2Int.zero;
        slotCost = 0;
        toDestroy = null;
        panel.SetActive(false);
    }
}
