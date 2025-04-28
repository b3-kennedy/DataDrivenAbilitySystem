using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class ProjectileData
{
    public string projectileName;
    public GameObject projectile;
}

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }
    public List<ProjectileData> projectiles = new List<ProjectileData>(); //technically dont need the list, but is nice for debugging
    private Dictionary<string, GameObject> projectileLookup = new Dictionary<string, GameObject>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] projectileArray = Resources.LoadAll<GameObject>("Prefabs/Projectiles");
        foreach (var proj in projectileArray)
        {
            ProjectileData data = new ProjectileData
            {
                projectileName = proj.name,
                projectile = proj
            };
            projectiles.Add(data);
            projectileLookup[proj.name] = proj;
        }
    }

    public GameObject CreateLinearProjectile(string name, Vector3 spawn ,Vector3 direction, float force, float duration, float damage, bool destroyOnHit)
    {
        if(projectileLookup.TryGetValue(name, out var prefab))
        {
            GameObject spawnedProjectile = Instantiate(prefab, spawn, Quaternion.identity);
            OnHit hitScript = spawnedProjectile.GetComponent<OnHit>();
            hitScript.SetDamage(damage);
            hitScript.SetDestroyOnHit(destroyOnHit);
            Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();
            rb.AddForce(direction * force, ForceMode.Impulse);
            Destroy(spawnedProjectile, duration);
            return spawnedProjectile;
        }
        return null;
    }

    public void CreateTrackingProjectile(string name, Vector3 spawn, Transform target, float force, float duration, float damage, bool destroyOnHit)
    {
        if(projectileLookup.TryGetValue(name, out var prefab))
        {
            GameObject spawnedProjectile = Instantiate(prefab, spawn, Quaternion.identity);
            OnHit hitScript = spawnedProjectile.GetComponent<OnHit>();
            hitScript.SetDamage(damage);
            hitScript.SetDestroyOnHit(destroyOnHit);
            var track = spawnedProjectile.AddComponent<TrackTarget>();
            track.target = target;
            track.force = force;
            Destroy(spawnedProjectile, duration);
        }
    }

    // will need to do a non physics implementation
    public GameObject CreateArcingProjectile(string name, Vector3 spawn, Vector3 target, float duration, float height,float damage, bool destroyOnHit)
    {
        if (projectileLookup.TryGetValue(name, out var prefab))
        {
            GameObject spawnedProjectile = Instantiate(prefab, spawn, Quaternion.identity);

            // Setup OnHit behavior
            OnHit hitScript = spawnedProjectile.GetComponent<OnHit>();
            hitScript.SetDamage(damage);
            hitScript.SetDestroyOnHit(destroyOnHit);

            Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            Arc arcScript = spawnedProjectile.AddComponent<Arc>();
            arcScript.Initialize(spawn, target, duration, height);

            Destroy(spawnedProjectile, duration);

            return spawnedProjectile;
        }

        return null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
