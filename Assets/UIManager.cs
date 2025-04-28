using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject canvas;

    public Transform abilityIconParent;

    List<GameObject> abilityIcons = new List<GameObject>();
    List<Image> abilityIconCooldownIndicators = new List<Image>();

    public GameObject hero;

    Abilities heroAbilities;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {

    }

    public void AddAbilityIcons()
    {
        for (int i = 0; i < abilityIconParent.childCount; i++)
        {
            abilityIcons.Add(abilityIconParent.GetChild(i).gameObject);
            abilityIconCooldownIndicators.Add(abilityIconParent.GetChild(i).GetChild(0).GetComponent<Image>());
        }
        heroAbilities = hero.GetComponent<Abilities>();
    }

    void Update()
    {
        for (int i = 0; i < abilityIcons.Count; i++)
        {
            var ability = heroAbilities.abilities[i].ability;
            var cooldownImage = abilityIconCooldownIndicators[i];
            if(ability.IsOnCooldown())
            {
                cooldownImage.gameObject.SetActive(true);
                cooldownImage.fillAmount = ability.GetCooldownProgress();
            }
            else
            {
                cooldownImage.gameObject.SetActive(false);
            }
            
        }
    }



}
