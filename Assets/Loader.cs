using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AbilityData
{
    public string abilityName;
    public int cooldown;

    public string hotkey;

    public string icon;

    public float castrange;

    public float radius;

    public float damage;

    public List<SpecialValue> specialValues;

    public string abilityDescription;
     

}

[System.Serializable]
public class SpecialValue
{
    public string name;
    public string value;
}

[System.Serializable]
public class HeroData
{
    public string prefab;
    public string heroName;
    public List<string> abilities;

}

//Loads and sets the necessary data for a hero
public class Loader : MonoBehaviour
{
    string heroName;
    Abilities abilityScript;

    HeroData heroData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    public void SetHeroData(HeroData data)
    {
        Debug.Log("Loading Hero Data");
        heroData = data;
        abilityScript = GetComponent<Abilities>();
        UIManager.Instance.hero = gameObject;
        LoadAbilities();
    }

    void LoadAbilities()
    {
        for (int i = 0; i < heroData.abilities.Count; i++)
        {
            string abilityName = heroData.abilities[i];

            var abilityTextAsset = Resources.Load<TextAsset>("Abilities/JSON/" + abilityName);
            AbilityData abilityData = JsonUtility.FromJson<AbilityData>(abilityTextAsset.text);

            var loadedAbility = Resources.Load<Ability>("Abilities/" + abilityName);
            if (loadedAbility == null)
            {
                Debug.LogError("Ability prefab for " + abilityName + " not found!");
                continue;
            }
            
            loadedAbility.SetCooldown(abilityData.cooldown);
            loadedAbility.SetIndex(i);
            loadedAbility.SetCastRange(abilityData.castrange);
            loadedAbility.SetDamage(abilityData.damage);
            loadedAbility.SetSpecialValues(abilityData.specialValues);

            if (loadedAbility is AOEAbility aoe)
            {
                aoe.SetRadius(abilityData.radius);
            }

            var abilityIconPrefab = Resources.Load<GameObject>("Prefabs/AbilityIconPrefab");
            GameObject spawnedIcon = Instantiate(abilityIconPrefab, UIManager.Instance.abilityIconParent);
            var abilityIconSprite = Resources.Load<Sprite>("Abilities/Icons/" + abilityData.icon);
            spawnedIcon.GetComponent<Image>().sprite = abilityIconSprite;

            loadedAbility.SetIcon(spawnedIcon);
            loadedAbility.GetIcon().GetComponent<Tooltip>().SetUpToolTip(abilityData);

            var hotKeyAbility = new HotkeyAbility
            {
                hotKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), abilityData.hotkey),
                ability = loadedAbility
            };
            abilityScript.abilities.Add(hotKeyAbility);
        }

        UIManager.Instance.AddAbilityIcons();
    }

}
