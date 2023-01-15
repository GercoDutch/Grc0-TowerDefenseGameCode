using UnityEngine;

public class Tower_Support : Tower_Base, ISupportive
{
    protected override void Update()
    {
        base.Update();
        
        GiveSupport(towerStats.Damage);
    }

    public void Support(float _value, GameObject _target)
    {
        var suppy = _target.transform.GetChild(1).gameObject.GetComponent<ISupportable>();

        if(suppy != null)
        {
            suppy.GetSupport(_value);
        }
    }

    public void GiveSupport(float _value)
    {
        targetsInRange.ForEach(target => Support(_value, target.gameObject));
    }

    protected override void UpgradeTower(Mesh _top, int _level)
    {
        base.UpgradeTower(_top, _level);

        // Upgrade tower stats
    }
}