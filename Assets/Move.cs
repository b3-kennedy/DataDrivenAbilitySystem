using TMPro;
using UnityEngine;

public class Move : MonoBehaviour
{

    Vector3 movePos;
    bool move = false;

    public float speed = 10;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetMovePos(Vector3 pos)
    {
        movePos = pos;
        move = true;
    }


    public void StopMove()
    {
        rb.linearVelocity = Vector3.zero;
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            Vector3 dir = (Action.GetCorrectVector(transform.position) - movePos).normalized;
            rb.linearVelocity = -dir * speed;
            if(Vector3.Distance(Action.GetCorrectVector(transform.position), movePos) <= 0.25f)
            {
                StopMove();
            }
        }
    }
}
