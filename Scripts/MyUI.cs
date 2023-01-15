using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MyUI : MonoBehaviour
{
    public static UnityEvent<bool> shopActivated = new();
    public static UnityEvent<int> healthValueChanged = new();
    public static UnityEvent<int> moneyValueChanged = new();

    [SerializeField] private GameObject myShop;

    [SerializeField] private SO_Tower[] towerInfo;

    [SerializeField] private Button[] shopButtons;

    [SerializeField] private Slider healthBar;

    [SerializeField] private TextMeshProUGUI moneyValue;

    private void Awake()
    {
        shopActivated.AddListener(ActivateShop);
        healthValueChanged.AddListener(ChangeHealthBarValue);
        moneyValueChanged.AddListener(ChangeMoneyValue);
    }

    private void Start()
    {
        // Setup shop buttons
        for(int i = 0; i < towerInfo.Length; i++) 
        {
            GameObject towerName = shopButtons[i].transform.GetChild(0).GetChild(0).gameObject;
            towerName.GetComponent<TextMeshProUGUI>().text = towerInfo[i].name;
            
            GameObject towerPrice = shopButtons[i].transform.GetChild(0).GetChild(1).gameObject;
            towerPrice.GetComponent<TextMeshProUGUI>().text = $"$ { towerInfo[i].Prices[0] }";
        }

        // Setup healthbar
        healthBar.maxValue = 1000;
        healthBar.value = 1000;
    }

    private void ActivateShop(bool _isActive)
    {
        myShop.SetActive(_isActive);
    }

    private void ChangeHealthBarValue(int _value)
    {
        if(_value != healthBar.value)
        {
            healthBar.value = _value;
        }
    }

    private void ChangeMoneyValue(int _value)
    {
        moneyValue.text = $"$ {_value}";
    }

    public void Btn_BuyTower(SO_Tower _tower)
    {
        Spawner_Tower.checkToBuy.Invoke(_tower);
    }

    public void Btn_UpgradeTower(int _level)
    {
        Spawner_Tower.checkToUpgrade.Invoke(_level);
    }
}