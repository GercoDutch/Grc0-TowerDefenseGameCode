using System.Collections;
using UnityEngine;

public class Tower_Fire : Tower_Base, ISupportable
{
    public float rotationSpeed = 400f;

    public Coroutine SuppRoutine { get; set;  }

    protected override void Update()
    {
        base.Update();

        Vector3 _rotation = new Vector3(0, rotationSpeed, 0);
        transform.eulerAngles += _rotation * Time.deltaTime;

        if (routine == null && targetsInRange != null)
            routine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        targetsInRange.ForEach(target => DoDamage(target.gameObject.GetComponent<IDamageable>(), towerStats.Damage));

        yield return new WaitForSeconds(towerStats.AttackDelay);
        routine = null;
    }

    public void DoDamage(IDamageable _target, int _value)
    {
        if (_target != null)
            _target.TakeDamage(_value);
    }

    public void GetSupport(float _value)
    {
        if (SuppRoutine != null)
            StopCoroutine(SuppRoutine);

        SuppRoutine = StartCoroutine(StartSupport(_value));
    }

    public IEnumerator StartSupport(float _value)
    {
        finalRange = towerStats.Range + _value / 2f;

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