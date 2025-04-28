using UnityEngine;

//projectile ability with a projectile that will track a target
public class Test3 : TargetedAbility
{
    public override void PerformCast()
    {
        StopMove();
        GameObject caster = GetCaster();
        Transform target = GetTarget();
        
        ProjectileManager.Instance.CreateTrackingProjectile("TestProjectile", caster.transform.position, target, 20, 10, GetDamage(), true);

        SetTarget(null);
    }
}
