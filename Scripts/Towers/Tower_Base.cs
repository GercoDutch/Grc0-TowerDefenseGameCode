using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

abstract public class Tower_Base : MonoBehaviour
{
    public UnityEvent<Mesh, int> towerUpgraded = new();

    [SerializeField] public SO_Tower towerStats;
    [SerializeField] protected int level;

    [SerializeField] protected List<Collider> targetsInRange;
    [SerializeField] protected Transform targetWithPriority;

    [SerializeField] private Transform crystal;

    public LayerMask targetMask;

    protected Coroutine routine;

    [SerializeField] protected float finalRange;

    private void Awake()
    {
        towerUpgraded.AddListener(UpgradeTower);
    }

    private void Start()
    {
        crystal = GameObject.FindGameObjectWithTag("Player").transform;
        finalRange = towerStats.Range;
    }

    protected virtual void Update()
    {
        transform.parent.GetChild(2).localScale = new Vector3(finalRange/0.7f*2, 0.1f, finalRange/0.7f*2);

        if (towerStats == null)
        {
            Debug.Log("This Tower doesn't have stats yet!");
            return;
        }

        CheckForTargets();
    }

    private void CheckForTargets()
    {
        float distanceOfClosestEnemy = 100f;
        targetsInRange = Physics.OverlapSphere(transform.position, finalRange, targetMask).ToList();
        targetsInRange.ForEach(target => {

            float distanceBetweenEnemyAndCrystal = Vector3.Distance(crystal.position, target.transform.position);
            if(distanceBetweenEnemyAndCrystal < distanceOfClosestEnemy)
            {
                targetWithPriority = target.transform;
                distanceOfClosestEnemy = distanceBetweenEnemyAndCrystal;
            }
        });

        if(targetWithPriority)
        {
            if(Vector3.Distance(transform.position, targetWithPriority.position) > towerStats.Range)
                targetWithPriority = null;
        }
    }

    private void OnDisable() => StopAllCoroutines();

    protected virtual void UpgradeTower(Mesh _top, int _level)
    {
        level = _level;
        
        ChangeTowerTop(_top);
    }

    protected void ChangeTowerTop(Mesh _top)
    {
        GetComponent<MeshFilter>().mesh = _top;
    }
}