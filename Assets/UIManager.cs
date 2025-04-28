using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject canvas;

    public GameObject abilityPanel;
    public Transform abilityIconParent;

    public GameObject pickPanel;

    List<GameObject> abilityIcons = new List<GameObject>();
    List<Image> abilityIconCooldownIndicators = new List<Image>();

    public GameObject hero;

    Abilities heroAbilities;

    public GameObject heroIconButtonPrefab;
    public Transform heroIconParent;

    public string selectedHero;

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

    public void PickHero()
    {
        GameManager.Instance.SetState(GameManager.GameState.PLAY);
        HeroSpawner.Instance.LoadHero(selectedHero);
    }

    public void OnPickState()
    {
        SetAbilityPanelVisibility(false);
        SetPickPanelVisibility(true);
    }

    public void OnPlayState()
    {
        SetAbilityPanelVisibility(true);
        SetPickPanelVisibility(false);
        
    }

    public void SelectHero(string heroName)
    {
        selectedHero = heroName;
        Debug.Log(selectedHero);
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

    public void SetPickPanelVisibility(bool value)
    {
        pickPanel.SetActive(value);
    }

    public void SetAbilityPanelVisibility(bool value)
    {
        abilityPanel.SetActive(value);
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
