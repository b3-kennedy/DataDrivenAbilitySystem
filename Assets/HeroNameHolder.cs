using UnityEngine;

public class HeroNameHolder : MonoBehaviour
{
    public string heroName;
    public void SetHero()
    {
        UIManager.Instance.SelectHero(heroName);
    }

}
