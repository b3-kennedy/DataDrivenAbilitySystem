using UnityEngine;

public class TeleportToOrb : ImmediateAbility
{
    GameObject projectile;

    public override void Start()
    {
        base.Start();
        SetActive(false);
        
    }

    public void SetProjectile(GameObject orb)
    {
        projectile = orb;
    }

    public override void PerformCast()
    {
        if(!projectile) return;

        StopMove();
        GameObject caster = GetCaster();
        Abilities abilityHolder = GetAbilityHolder();
        Ability orbAbility = abilityHolder.GetAbilityByName("TeleportOrb");
        if(orbAbility is TeleportOrb orb)
        {
            orb.OrbDestroyed();
        }
        caster.transform.position = projectile.transform.position;
        Destroy(projectile);
        SetActive(false);
    }
}
