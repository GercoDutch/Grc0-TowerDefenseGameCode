using UnityEngine;
using UnityEngine.Events;

public class Money : MonoBehaviour
{
    public static UnityEvent<int> getMoney = new();
    public static UnityEvent<SO_Tower> buyTower = new();
    public static UnityEvent<SO_Tower, int> upgradeTower = new();

    [SerializeField] private int moneyAmount;

    private void Start()
    {
        getMoney.AddListener(GetMoney);
        buyTower.AddListener(BuyTower);
        upgradeTower.AddListener(BuyTower);

        MyUI.moneyValueChanged.Invoke(moneyAmount);
    }

    private void ChangeMoneyAmount(int _amount)
    {
        moneyAmount += _amount;

        if(moneyAmount < 0)
            moneyAmount = 0;

        MyUI.moneyValueChanged.Invoke(moneyAmount);
    }

    private void GetMoney(int _value)
    {
        if(_value > 0)
            ChangeMoneyAmount(_value);
    }

    private void BuyTower(SO_Tower _tower)
    {
        if(CanIBuySomething(_tower.Prices[0]))
            Spawner_Tower.buyTower.Invoke(_tower);
    }

    private void BuyTower(SO_Tower _tower, int _level)
    {
        if (CanIBuySomething(_tower.Prices[_level]))
            Spawner_Tower.upgradeTower.Invoke(_level);
    }

    private bool CanIBuySomething(int _price)
    {
        bool b = _price <= moneyAmount;

        if(b) ChangeMoneyAmount(-_price);
        return b;
    }
}