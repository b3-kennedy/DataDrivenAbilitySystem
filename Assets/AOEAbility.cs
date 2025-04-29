using Unity.Properties;
using UnityEngine;

public class AOEAbility : Ability
{
    LineRenderer lr;
    float radius = 1;

    public override void OnStartCast()
    {
        base.OnStartCast();
        lr = Instantiate(lineRendererPrefab);

    }

    public void SetRadius(float rad)
    {
        radius = rad;
    }

    public float GetRadius()
    {
        return radius;
    }

    public override void Update()
    {
        if(IsOnCooldown()) return;

        base.Update();
        if(IsTargeting() && IsWithinRange())
        {
            DrawCircle(lr,GetCursorPosition(), radius);
        }
        
    }

    public override void DisableLineRenderer()
    {
        base.DisableLineRenderer();
        if(lr)
        {
            Destroy(lr.gameObject);
        }
        
    }

    public virtual Collider[] GetUnitsInArea()
    {
        Collider[] colliders = Physics.OverlapSphere(GetCursorPosition(), radius);
        return colliders;

    }

    public virtual Collider[] GetUnitsInAreaAroundCaster()
    {
        Collider[] colliders = Physics.OverlapSphere(Action.GetCorrectVector(GetCaster().transform.position), radius);
        return colliders;
    }
}
