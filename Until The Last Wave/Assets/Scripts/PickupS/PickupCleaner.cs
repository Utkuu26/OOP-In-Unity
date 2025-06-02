using UnityEngine;

public class PickupCleaner : MonoBehaviour
{
    public PickupSpawnManager manager;
    public Transform spawnPoint;

    void OnDestroy()
    {
        if (manager != null)
            manager.RemovePickup(gameObject);

        if (spawnPoint != null)
        {
            var sp = spawnPoint.GetComponent<SpawnPoint>();
            if (sp != null)
                sp.isOccupied = false;
        }
    }
}
