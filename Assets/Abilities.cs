using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

[System.Serializable]
public class HotkeyAbility
{
    public KeyCode hotKey;
    public Ability ability;

    
}

public class Abilities : MonoBehaviour
{

    public List<HotkeyAbility> abilities = new List<HotkeyAbility>();
    Ability currentAbility;

    public LineRenderer lr;
    public LayerMask mask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var a in abilities)
        {
            if(a.ability)
            {
                a.ability.SetCaster(gameObject);
                a.ability.SetLineRenderer(lr);
                a.ability.SetLayerMask(mask);
                a.ability.Start();
            }
            
        }
    }

    public Ability GetAbilityByName(string name)
    {
        foreach (var a in abilities)
        {
            if(a.ability.name == name)
            {
                return a.ability;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var a in abilities)
        {
            if(a.ability != currentAbility)
            {
                a.ability.SetTargeting(false);
                a.ability.DisableLineRenderer();
            }

            a.ability.Update();

            if(Input.GetKeyDown(a.hotKey) && a.ability.GetActive())
            {
                currentAbility = a.ability;
                a.ability.OnStartCast();
                

            }
        }
    }

    void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawSphere(transform.position, 10);
    }
}
