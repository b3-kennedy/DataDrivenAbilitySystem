using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : AOEAbility
{

    List<Transform> enemies = new List<Transform>();
    Vector3 pullCentre;
    float pullDuration;
    float pullElapsed;
    bool isPulling;

    public override void OnStartCast()
    {
        base.OnStartCast();
    }

    public override void PerformCast()
    {
        GameObject caster = GetCaster();
        Vector3 mousePos = GetCursorPosition();

        Collider[] cols = GetUnitsInArea();
        

        foreach (var col in cols)
        {
            if(col.CompareTag("enemy"))
            {
                enemies.Add(col.transform);
            }
        }

        StopMove();
        pullCentre = mousePos;
        pullDuration = GetSpecialFloatValue("pullDuration");
        pullElapsed = 0f;
        isPulling = true;
    }

    public override void Update()
    {
        base.Update();
        if (isPulling)
        {
            pullElapsed += Time.deltaTime;
            if (pullElapsed < pullDuration)
            {
                foreach (var unit in enemies)
                {
                    if (unit != null)
                    {
                        Vector3 direction = (pullCentre - unit.position).normalized;
                        float pullStrength = GetSpecialFloatValue("pullForce");
                        unit.position += pullStrength * Time.deltaTime * direction;
                    }
                }
            }
            else
            {
                isPulling = false;
                enemies.Clear();
            }
        }        
    }

}
