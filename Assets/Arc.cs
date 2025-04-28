using UnityEngine;

public class Arc : MonoBehaviour
{
    private Vector3 start;
    private Vector3 end;
    private float duration;
    private float height;
    private float elapsedTime = 0f;

    public void Initialize(Vector3 start, Vector3 end, float duration, float height)
    {
        this.start = start;
        this.end = end;
        this.duration = duration;
        this.height = height;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        // Horizontal linear interpolation
        Vector3 currentPos = Vector3.Lerp(start, end, t);

        // Vertical arc offset (parabola shape)
        float arc = 4 * height * t * (1 - t); // parabola formula (peaks at t=0.5)

        currentPos.y += arc;

        transform.position = currentPos;
    }
}
