using UnityEngine;

public class Lasso : TargetedAbility
{

    bool startTimer;
    float timer;

    Transform lassoTarget;
    Patrol move;
    float duration;
    public override void Start()
    {
        base.Start();
        duration = GetSpecialFloatValue("duration");
    }


    public override void PerformCast()
    {
        lassoTarget = GetTarget();
        GameObject caster = GetCaster();
        move = lassoTarget.GetComponent<Patrol>();
        if(move)
        {
            lassoTarget.GetComponent<Rigidbody>().isKinematic = true;
            move.enabled = false;
            lassoTarget.SetParent(caster.transform);
            startTimer = true;
        }
    }

    public override void Update()
    {
        base.Update();
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= duration)
            {
                lassoTarget.GetComponent<Rigidbody>().isKinematic = false;
                move.enabled = true;
                lassoTarget.SetParent(null);
                timer = 0;
                startTimer = false;
            }
        }

    }
}
