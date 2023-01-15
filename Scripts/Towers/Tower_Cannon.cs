using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower_Cannon : Tower_Base, IDamaging, ISupportable
{
    private const float explosionRange = 2f;

    public Coroutine SuppRoutine { get ; set ; }

    protected override void Update()
    {
        base.Update();

        if(targetWithPriority)
        {
            RotateToTarget(targetWithPriority);
        }

        if (routine == null && targetWithPriority != null)
            routine = StartCoroutine(Attack());
    }

    private void RotateToTarget(Transform target)
    {
        Transform child = transform.GetChild(1);

        Vector3 targetDirection = (target.position - child.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        child.rotation = Quaternion.Slerp(child.rotation, lookRotation, Time.deltaTime * 8f);
    }

    private IEnumerator Attack()
    {
        Explosion();
        yield return new WaitForSeconds(towerStats.AttackDelay);
        routine = null;
    }
     
    private void Explosion()
    {
        List<Collider> enemiesInExplosionRange = Physics.OverlapSphere(targetWithPriority.transform.position, explosionRange, targetMask).ToList();
        enemiesInExplosionRange.ForEach(enemy => DoDamage(enemy.gameObject.GetComponent<IDamageable>(), towerStats.Damage));
    }


    // IDamaging
    public void DoDamage(IDamageable _target, int _value) 
    {
        if (_target != null)
            _target.TakeDamage(_value);
    }

    // ISupportable
    public void GetSupport(float _value)
    {
        if (SuppRoutine != null)
            StopCoroutine(SuppRoutine);

        SuppRoutine = StartCoroutine(StartSupport(_value));
    }

    public IEnumerator StartSupport(float _value)
    {
        finalRange = towerStats.Range + _value;

        yield return new WaitForSeconds(1);

        finalRange = towerStats.Range;
        SuppRoutine = null;
    }

    protected override void UpgradeTower(Mesh _top, int _level)
    {
        base.UpgradeTower(_top, _level);

        // Upgrade tower stats
    }
}