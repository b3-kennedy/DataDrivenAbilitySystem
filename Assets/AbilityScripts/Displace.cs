using UnityEngine;

public class Displace : AOEAbility
{

    GameObject proj;
    Vector3 startCursorPos;
    public override void PerformCast()
    {
        StopMove();
        startCursorPos = new Vector3(GetCursorPosition().x, 0, GetCursorPosition().z);
        GameObject caster = GetCaster();

        Vector3 dir = (startCursorPos - Action.GetCorrectVector(caster.transform.position)).normalized;

        float heightMultiplier = GetSpecialFloatValue("arcHeightMultiplier");
        float durationMultiplier = GetSpecialFloatValue("durationMultiplier");
        float height = Vector3.Distance(caster.transform.position, startCursorPos)/heightMultiplier;
        float duration = Vector3.Distance(caster.transform.position, startCursorPos)/durationMultiplier;

        proj = ProjectileManager.Instance.CreateArcingProjectile("DisplaceProjectile", caster.transform.position, startCursorPos, duration ,height, GetDamage(), true);

    }

    public override void Update()
    {
        base.Update();
        if(proj)
        {
            if(Vector3.Distance(proj.transform.position, startCursorPos) <= 1)
            {
                float displaceForce = GetSpecialFloatValue("displaceForce");
                OnHit hit = proj.GetComponent<OnHit>();
                Collider[] cols = hit.GetUnitsInRadius(GetRadius());

                foreach (var col in cols)
                {
                    Rigidbody rb = col.GetComponent<Rigidbody>();
                    Patrol patrol = col.GetComponent<Patrol>();
                    if(rb && !rb.isKinematic)
                    {
                        if(patrol)
                        {
                            patrol.PauseMovement(.5f);
                        }
                        rb.linearVelocity = Vector3.zero;
                        Vector3 dir = (col.transform.position - proj.transform.position).normalized;
                        rb.AddForce(dir * displaceForce, ForceMode.Impulse);
 
                    }
                }
                Destroy(proj);
            }
        }
    }
}
