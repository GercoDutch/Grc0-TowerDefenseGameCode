using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tower", menuName = "Tower")]
public class SO_Tower : ScriptableObject
{
    [SerializeField] private Mesh weaponModel;
    [SerializeField] private Material[] weaponMaterials;

    [SerializeField] private int[] prices;
    [SerializeField] private int damage; // schade per x aanvallen
    
    [SerializeField] private float fireRate; // N x aanvallen per seconde
    [SerializeField] private float range; // afstand waarop Tower kan aanvallen
    [SerializeField] private float[] upgradeStats;
    

    public Material[] WeaponMaterials { get => weaponMaterials; }
    public Mesh WeaponModel { get => weaponModel; }

    public int[] Prices { get => prices; }
    public int Damage { get => damage; }
    
    public float AttackDelay { get => 1f / fireRate; }
    public float Range
    {
        get => range;
        set => range = value;
    }
    public float[] UpgradeStats { get => upgradeStats; }
}