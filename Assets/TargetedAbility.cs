using UnityEngine;
using System.Linq;

public class TargetedAbility: Ability
{
    Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
        
    }

    public Transform GetTarget()
    {
        return target;
    }
}