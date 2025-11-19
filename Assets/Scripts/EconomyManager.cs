using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    public int money = 500;
    public TMP_Text moneyText;

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

    private void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = "Saldo: " + money;
    }
}
