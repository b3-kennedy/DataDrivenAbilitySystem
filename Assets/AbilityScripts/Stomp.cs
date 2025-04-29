using UnityEngine;

public class Stomp : ImmediateAOEAbility
{
    public override void PerformCast()
    {
        Collider[] units = GetUnitsInAreaAroundCaster();
        foreach (var unit in units)
        {
            if(!unit.CompareTag("enemy")) continue;

            Health health = unit.GetComponent<Health>();
            if(health)
            {
                health.TakeDamage(GetDamage());
            }
        }

    }
}
