using UnityEngine;

public class Action : MonoBehaviour
{

    Move move;
    Abilities abilities;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = GetComponent<Move>();
        abilities = GetComponent<Abilities>();
    }


    public static Vector3 GetCorrectVector(Vector3 vector)
    {
        return new Vector3(vector.x, 1, vector.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                switch(hit.collider.tag)
                {
                    case "floor":
                    move.SetMovePos(GetCorrectVector(hit.point));
                    StopTargeting();
                    
                    break;
                }
            }
        }

        if(Input.GetButtonDown("Fire1"))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (var a in abilities.abilities)
                {
                    if(a.ability.IsTargeting())
                    {
                        if (a.ability is TargetedAbility targetedAbility)
                        {
                            if(hit.collider.tag == "enemy")
                            {   
                                targetedAbility.SetTarget(hit.collider.transform);
                                a.ability.Cast();
                                a.ability.SetTargeting(false);
                                return;
                            }
                            
                        }
                        else
                        {
                            a.ability.Cast();
                            a.ability.DisableLineRenderer();
                            a.ability.SetTargeting(false);
                        }

                    }
                    a.ability.DisableLineRenderer();
                    a.ability.SetTargeting(false);
                }
            }
            StopTargeting();
        }
    }

    void StopTargeting()
    {
        foreach(var a in abilities.abilities)
        {
            if(a.ability.IsTargeting())
            {
                a.ability.DisableLineRenderer();
                a.ability.SetTargeting(false);
            }
        }
    }
}
