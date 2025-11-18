using UnityEngine;

public class BuySlot : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

    public void BuyNewSlot()
    {
        if (EconomyManager.Instance.SpendMoney(50))
        {
            Instantiate(slotPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
