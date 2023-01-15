using UnityEngine;
using System.Collections;

// Do & Take Damage
public interface IDamaging
{
    public void DoDamage(IDamageable _target, int _value);
}

public interface IDamageable
{
    public void TakeDamage(int _value);
}


// Give & Get Support
public interface ISupportive
{
    public void GiveSupport(float _value);
}

public interface ISupportable
{
    public Coroutine SuppRoutine { get; set; }
    public void GetSupport(float _value);
}