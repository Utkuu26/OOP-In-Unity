using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupEntry
{
    public GameObject prefab;
    public int weight;
}

public class PickupSpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public List<PickupEntry> pickups;

    public float spawnInterval = 10f;
    public int maxActivePickups = 5;

    private List<GameObject> activePickups = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnPickup), 2f, spawnInterval);
    }

    void SpawnPickup()
    {
        if (activePickups.Count >= maxActivePickups)
            return;

        List<Transform> freePoints = new List<Transform>();
        foreach (var point in spawnPoints)
        {
            var sp = point.GetComponent<SpawnPoint>();
            if (sp != null && !sp.isOccupied)
                freePoints.Add(point);
        }

        if (freePoints.Count == 0)
            return; 

        Transform chosenPoint = freePoints[Random.Range(0, freePoints.Count)];
        GameObject prefabToSpawn = GetWeightedRandomPickup();

        GameObject newPickup = Instantiate(prefabToSpawn, chosenPoint.position, Quaternion.identity);
        activePickups.Add(newPickup);

        chosenPoint.GetComponent<SpawnPoint>().isOccupied = true;

        PickupCleaner cleaner = newPickup.AddComponent<PickupCleaner>();
        cleaner.manager = this;
        cleaner.spawnPoint = chosenPoint;
    }


    public void RemovePickup(GameObject pickup)
    {
        if (activePickups.Contains(pickup))
            activePickups.Remove(pickup);
    }

    GameObject GetWeightedRandomPickup()
    {
        int totalWeight = 0;
        foreach (var entry in pickups)
            totalWeight += entry.weight;

        int randomValue = Random.Range(0, totalWeight);
        int cumulative = 0;

        foreach (var entry in pickups)
        {
            cumulative += entry.weight;
            if (randomValue < cumulative)
                return entry.prefab;
        }

        return null;
    }
}
