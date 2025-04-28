using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHealth;
    float health;

    public RectTransform healthBar;

    float healthBarStartWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthBarStartWidth = healthBar.sizeDelta.x;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        UpdateHealthBar();
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    } 


    void UpdateHealthBar()
    {
        float percent = health/maxHealth;
        float width = healthBarStartWidth * percent;
        healthBar.sizeDelta = new Vector2(width, healthBar.sizeDelta.y);
    }
}
