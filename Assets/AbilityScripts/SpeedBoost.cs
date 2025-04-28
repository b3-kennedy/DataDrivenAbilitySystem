using UnityEngine;

public class SpeedBoost : ImmediateAbility
{
    bool startTimer = false;
    float timer;

    Move move;

    float baseSpeed;

    float duration;
    public override void Start()
    {
        base.Start();
        duration = GetSpecialFloatValue("duration");

    }
    public override void PerformCast()
    {   
        GameObject caster = GetCaster();
        move = caster.GetComponent<Move>();
        baseSpeed = move.speed;
        float speedMultiplier = GetSpecialFloatValue("boost");
        float newSpeed = move.speed * speedMultiplier;
        move.speed = newSpeed;
        startTimer = true;
    }

    public override void Update()
    {
        base.Update();
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= duration)
            {
                move.speed = baseSpeed;
                startTimer = false;
                timer = 0;
            }
        }
    }
}
