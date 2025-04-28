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

[System.Serializable]
public class AbilityDatabase
{
    public List<AbilityData> abilities;
}

//Loads and sets the necessary data for a hero
public class Loader : MonoBehaviour
{

    
    public AbilityDatabase abilityDatabase;
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
        abilityDatabase = LoadAbilityDatabase();
        LoadAbilities();
    }

    AbilityDatabase LoadAbilityDatabase()
    {
        var abilityDB = Resources.Load<TextAsset>("Abilities/Abilities");
        return JsonUtility.FromJson<AbilityDatabase>(abilityDB.text);
    }

    void LoadAbilities()
    {
        foreach (var abilityName in heroData.abilities)
        {
            // Find ability data by name
            var abilityData = abilityDatabase.abilities.Find(ab => ab.abilityName == abilityName);

            if (abilityData == null)
            {
                Debug.LogError("Ability " + abilityName + " not found in AbilityDatabase!");
                continue;
            }

            string path = "Abilities/" + abilityName;
            var loadedAbility = Resources.Load<Ability>(path);

            loadedAbility.SetCooldown(abilityData.cooldown);
            loadedAbility.SetIndex(abilityScript.abilities.Count);
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
