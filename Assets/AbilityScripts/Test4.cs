using UnityEngine;


//An Area of effect abbility that does damage
public class Test4 : AOEAbility
{
    public override void PerformCast()
    {
        StopMove();
        Collider[] cols = GetUnitsInArea();
        foreach (var col in cols)
        {
            if(col.CompareTag("enemy"))
            {

                if(col.GetComponent<Health>())
                {
                    col.GetComponent<Health>().TakeDamage(GetDamage());
                }
            }
        }
    }
}
