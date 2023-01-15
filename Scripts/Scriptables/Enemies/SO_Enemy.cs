using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy", menuName = "Enemy")]
public class SO_Enemy : ScriptableObject
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material[] materials;

    [SerializeField] private float speed;
    [SerializeField] private float fireRate; // N x aanvallen per seconde
    [SerializeField] private float range;

    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private int worth;

    [SerializeField] private bool selfDestruct;

    public Mesh Mesh { get => mesh; }
    public Material[] Materials { get => materials; }

    public float Speed { get => speed; }
    public float AttackDelay { get => 1f / fireRate; }
    public float Range { get => range; }

    public int MaxHealth { get => maxHealth; }
    public int Damage { get => damage; }
    public int Worth { get => worth; }

    public bool SelfDestruct { get => selfDestruct; }
}
