using UnityEngine;

public class Swap : TargetedAbility
{
    public override void PerformCast()
    {
        GameObject caster = GetCaster();
        Transform target = GetTarget();
        Vector3 casterPos = caster.transform.position;
        Vector3 targetPos = target.position;

        caster.transform.position = targetPos;
        target.position = casterPos;

        target.GetComponent<Health>().TakeDamage(GetDamage());

    }
}
