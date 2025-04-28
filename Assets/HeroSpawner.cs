using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ValidHeroes
{
    public List<string> validHeroes;
}


public class HeroSpawner : MonoBehaviour
{
    public static HeroSpawner Instance;
    HeroData heroData;
    ValidHeroes valHeroes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        TextAsset heroes = Resources.Load<TextAsset>("Heroes/heroes");
        valHeroes = JsonUtility.FromJson<ValidHeroes>(heroes.text);
        UIManager uiManager = UIManager.Instance;
        for (int i = 0; i < valHeroes.validHeroes.Count; i++)
        {
            string heroName = valHeroes.validHeroes[i];
            GameObject spawnedIcon = Instantiate(uiManager.heroIconButtonPrefab, uiManager.heroIconParent);
            Sprite icon = Resources.Load<Sprite>("Heroes/Icons/"+heroName);
            spawnedIcon.GetComponent<Image>().sprite = icon;
            HeroNameHolder heroNameHolder = spawnedIcon.GetComponent<HeroNameHolder>();
            heroNameHolder.heroName = heroName;
            
        }

    }

    public void LoadHero(string heroName)
    {
        Physics.IgnoreLayerCollision(3,6);
        Debug.Log("Spawning Hero");
        TextAsset data = Resources.Load<TextAsset>("Heroes/" + heroName);
        heroData = JsonUtility.FromJson<HeroData>(data.text);
        string path = "Prefabs/"+heroData.prefab;
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject spawnedPrefab = Instantiate(prefab, new Vector3(0,1f,0), Quaternion.identity);
        spawnedPrefab.GetComponent<Loader>().SetHeroData(heroData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
