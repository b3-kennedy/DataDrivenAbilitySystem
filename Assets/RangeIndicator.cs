using UnityEngine;

public class RangeIndicator : MonoBehaviour
{


    public void SetPosition(Vector3 position)
    {
        transform.GetChild(0).position = position;
    }

    public void SetRadius(float radius)
    {
        transform.GetChild(0).localScale = new Vector3(radius/5.5f, radius/5.5f, 1);
    }

    public void HideIndicator()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowIndicator()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
