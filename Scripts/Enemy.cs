using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamaging, IDamageable
{
    [Header("References")]
    [SerializeField] private Transform target;

    public Transform spawn;
    [SerializeField] private List<Transform> path;

    public int myWaveIndex;

    [Header("Stats")]
    [SerializeField] private SO_Enemy enemyStats;
    [SerializeField] private int currentHealth;
    private int finalHealth;

    private Coroutine routine;
    public Animator anim;


    void Start()
    {
        currentHealth = enemyStats.MaxHealth + (myWaveIndex * 10);

        target = GameObject.FindGameObjectWithTag("Player").transform;
        foreach(Transform transform in spawn)
        {
            path.Add(transform);
        }
    }

    void Update()
    {
        if (path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[0].position, enemyStats.Speed * Time.deltaTime);
            
            RotateToTarget(path[0]);

            if (Vector3.Distance(transform.position, path[0].position) < 0.1f)
            {
                path.RemoveAt(0);
            }
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (routine == null && distance < enemyStats.Range)
        {
            routine = StartCoroutine(IsAttacking());
            path.Clear();
            Debug.Log("attackRange");
        }
    }

    private void RotateToTarget(Transform target)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);
    }


    // Do Damage
    public void DoDamage(IDamageable _target, int _value)
    {
        if(_target != null)
        {
            _target.TakeDamage(_value);

            if (enemyStats.SelfDestruct)
                Die();
        }
    }

    private IEnumerator IsAttacking()
    {
        DoDamage(target.GetComponent<IDamageable>(), enemyStats.Damage);

        yield return new WaitForSeconds(enemyStats.AttackDelay);
        routine = null;

        anim.SetBool("Attack", true);
    }

    public void TakeDamage(int _value)
    {
        currentHealth -= _value;

        // Change color for a second
        StartCoroutine(ChangeColor());

        if (currentHealth <= 0)
            Die();
    }

    private IEnumerator ChangeColor()
    {
        transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(0.25f);

        transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
    }

    private void Die()
    {
        Money.getMoney.Invoke(enemyStats.Worth);
        Destroy(gameObject);
    }
}