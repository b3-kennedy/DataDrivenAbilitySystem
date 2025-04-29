using UnityEngine;

public class Burn : TargetedAbility
{
    bool startBurn;
    float burnTimer = 0;

    float burnIntervalTimer = 0;

    float burnDuration;
    float burnInterval;

    Transform burnTarget;

    public override void Start()
    {
        base.Start();
        burnIntervalTimer = 0;
        burnTimer = 0;
    }
    
    public override void PerformCast()
    {
        burnTarget = GetTarget();
        burnDuration = GetSpecialFloatValue("duration");
        burnInterval = GetSpecialFloatValue("interval");
        burnTarget.GetComponent<Health>().TakeDamage(GetDamage());
        startBurn = true;


    }

    public override void Update()
    {
        base.Update();
        if(startBurn && burnTarget)
        {
            burnTimer += Time.deltaTime;
            burnIntervalTimer += Time.deltaTime;
            if(burnIntervalTimer >= burnInterval)
            {
                burnTarget.GetComponent<Health>().TakeDamage(GetDamage());
                burnIntervalTimer = 0;
            }

            if(burnTimer >= burnDuration)
            {
                startBurn = false;
                burnTarget = null;
                burnTimer = 0;
                burnIntervalTimer = 0;
            }
        }
    }
}
