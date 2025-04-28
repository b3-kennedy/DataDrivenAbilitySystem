using UnityEngine;

//basic teleport ability
public class Test2 : Ability
{
    public override void PerformCast()
    {
        StopMove();
        GameObject caster = GetCaster();
        Vector3 cursorPos = GetCursorPosition();

        caster.transform.position = cursorPos;
    }
}
