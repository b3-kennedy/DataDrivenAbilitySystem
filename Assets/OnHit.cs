using UnityEngine;

public class OnHit : MonoBehaviour
{
    float damage;
    Collider colldier;

    bool destroyOnHit = true;

    bool isTrigger;

    void Start()
    {
        colldier = GetComponent<Collider>();
        if(colldier.isTrigger)
        {
            isTrigger = true;
        }
        else
        {
            isTrigger = false;
        }
    }

    public void SetDestroyOnHit(bool value)
    {
        destroyOnHit = value;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(isTrigger) return;

        if(other.collider.CompareTag("enemy"))
        {
            var health = other.collider.GetComponent<Health>();
            health.TakeDamage(damage);
            if(destroyOnHit)
            {
                Destroy(gameObject);
            }
            
        }
    }

    public Collider[] GetUnitsInRadius(float radius)
    {
        return Physics.OverlapSphere(transform.position, radius);
    }

    void OnTriggerEnter(Collider other)
    {
        if(!isTrigger) return;

        if(other.CompareTag("enemy"))
        {
            var health = other.GetComponent<Health>();
            health.TakeDamage(damage);
            if(destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
