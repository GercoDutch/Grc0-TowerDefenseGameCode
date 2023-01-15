using System;
using UnityEngine;
using UnityEngine.Events;

public class Spawner_Tower : MonoBehaviour
{
    public static UnityEvent<SO_Tower> buyTower = new();
    public static UnityEvent<int> upgradeTower = new();

    public static UnityEvent<SO_Tower> checkToBuy = new();
    public static UnityEvent<int> checkToUpgrade = new();

    [SerializeField] private Mesh[] towerTops;

    [SerializeField] private Transform playerCamera;

    [SerializeField] private GameObject chosenTower;
    [SerializeField] private Color chosenTowerColor;
    [SerializeField] private LayerMask towerMask;

    [SerializeField] private LayerMask enemyMask;

    private void Awake()
    {
        buyTower.AddListener(BuyTower);
        upgradeTower.AddListener(UpgradeTower);

        checkToBuy.AddListener(CheckIfICanPay);
        checkToUpgrade.AddListener(CheckIfICanPay);
    }
    
    private void Update()
    {
        if(Input.GetButtonDown("Fire1")) SelectTower();
    }

    private void SelectTower()
    {
        if (chosenTower)
        {
            chosenTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = chosenTowerColor;
            chosenTower.transform.GetChild(2).gameObject.SetActive(false);
        }

        Ray mouseRay = playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, towerMask))
        {
            chosenTower = hit.transform.gameObject;
            chosenTowerColor = chosenTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
            chosenTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1f, 0, 0, 1f);
            chosenTower.transform.GetChild(2).gameObject.SetActive(true);

            MyUI.shopActivated.Invoke(true);
        }
    }

    private void PlaceTower(Type _towerScript, SO_Tower _towerType)
    {
        GameObject towerTop = chosenTower.transform.GetChild(1).gameObject;
        Tower_Base towerScript = towerTop.GetComponent<Tower_Base>();

        if(!towerScript)
        {
            towerTop.AddComponent(_towerScript);
            var script = towerTop.GetComponent<Tower_Base>();
            script.towerStats = _towerType;
            
            if(_towerScript == typeof(Tower_Support))
                script.targetMask = towerMask;
            else
                script.targetMask = enemyMask;

            towerTop.transform.GetChild(1).GetComponent<MeshFilter>().mesh = _towerType.WeaponModel;
            towerTop.transform.GetChild(1).GetComponent<MeshRenderer>().materials = _towerType.WeaponMaterials;

            chosenTower.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = chosenTowerColor;
            chosenTower = null;

            MyUI.shopActivated.Invoke(false);
        }
        towerTop.SetActive(true);
    }

    private void BuyTower(SO_Tower _tower)
    {
        if (!chosenTower)
            return;

        Type towerType = typeof(Tower_Turret);
        string towerName = _tower.name;
        Debug.Log(towerName);

        switch(towerName)
        {
            case "Bolt":
                towerType = typeof(Tower_Turret);
                break;
            case "Fire":
                towerType = typeof(Tower_Fire);
                break;
            case "Cannon":
                towerType = typeof(Tower_Cannon);
                break;
            case "Support":
                towerType = typeof(Tower_Support);
                break;
        }
        PlaceTower(towerType, _tower);
    }

    private void UpgradeTower(int _level)
    {
        Tower_Base towerScript = chosenTower.transform.GetChild(0).GetComponent<Tower_Base>();
        towerScript.towerUpgraded.Invoke(towerTops[_level], _level);
    }

    public void SellTower()
    {
        if(chosenTower)
        {
            GameObject towerTop = chosenTower.transform.GetChild(1).gameObject;

            Tower_Base script = towerTop.GetComponent<Tower_Base>();
            if (script)
            {
                Money.getMoney.Invoke(script.towerStats.Prices[0]);
                Destroy(script);
            }

            towerTop.transform.GetChild(1).GetComponent<MeshFilter>().mesh = null;
            // towerTop.transform.GetChild(1).GetComponent<MeshRenderer>().materials = null;

            towerTop.SetActive(false);
            chosenTower.transform.GetChild(2).localScale = new Vector3(2, 0.1f, 2);
        }
    }

    private void CheckIfICanPay(int _level)
    {
        Tower_Base towerScript = chosenTower.transform.GetChild(0).GetComponent<Tower_Base>();
        Money.upgradeTower.Invoke(towerScript.towerStats, _level);
    }

    private void CheckIfICanPay(SO_Tower _tower)
    {
        Money.buyTower.Invoke(_tower);
    }
}