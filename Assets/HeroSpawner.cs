using UnityEngine;

public class HeroSpawner : MonoBehaviour
{

    HeroData heroData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.IgnoreLayerCollision(3,6);
        Debug.Log("Spawning Hero");
        TextAsset data = Resources.Load<TextAsset>("Heroes/Hero1");
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
