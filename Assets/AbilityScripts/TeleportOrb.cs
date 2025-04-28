using UnityEngine;

public class TeleportOrb : Ability
{

    Ability teleportToOrb;
    float timer = 0;

    bool startTimer = false;

    public override void PerformCast()
    {
        Vector3 cursorPos = GetCursorPosition();
        GameObject caster = GetCaster();
        Abilities abilityHolder = GetAbilityHolder();
        float duration = GetSpecialFloatValue("duration");
        float force = GetSpecialFloatValue("force");
        Vector3 dir = (cursorPos - Action.GetCorrectVector(caster.transform.position)).normalized;

        StopMove();
        GameObject projectile = ProjectileManager.Instance.CreateLinearProjectile("TeleportOrbProjectile", caster.transform.position, dir, force, duration, GetDamage(), false);

        teleportToOrb = abilityHolder.GetAbilityByName("TeleportToOrb");
        if(teleportToOrb)
        {
            if(teleportToOrb is TeleportToOrb tpOrb)
            {
                teleportToOrb.SetActive(true);
                tpOrb.SetProjectile(projectile);
            }
            
        }
        startTimer = true;
    }

    public void OrbDestroyed()
    {
        startTimer = false;
        timer = 0;
    }

    public override void Update()
    {
        base.Update();
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= 10)
            {
                if(teleportToOrb)
                {
                    if(teleportToOrb is TeleportToOrb tpOrb)
                    {
                        tpOrb.SetProjectile(null);
                        teleportToOrb.SetActive(false);
                        timer = 0;
                        startTimer = false;
                    }
                    
                }
            }
        }


    }
}
