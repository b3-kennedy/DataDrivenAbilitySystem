using UnityEngine;

public class TrackTarget : MonoBehaviour
{

    public float force;
    public Transform target;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (target.position - Action.GetCorrectVector(transform.position)).normalized;
        rb.linearVelocity = dir * force;

        if(Vector3.Distance(transform.position, target.position) <= 0.55f)
        {
            Destroy(gameObject);
        }

    }
}
