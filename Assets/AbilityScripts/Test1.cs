using UnityEngine;


//basic projectile skillshot ability
public class Test1 : Ability
{
    public override void PerformCast()
    {
        StopMove();
        Vector3 cursorPos = GetCursorPosition();
        GameObject caster = GetCaster();

        Vector3 dir = (cursorPos - Action.GetCorrectVector(caster.transform.position)).normalized;

        ProjectileManager.Instance.CreateLinearProjectile("TestProjectile", caster.transform.position, dir, 10, 10, GetDamage(), true);

        Debug.Log($"{caster.name} casted {name}");
    }
}
