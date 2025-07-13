using System.Collections;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public ObjectRandomizer randomizer;
    private int spawnedCanisters;
    public bool canSpawn;
    public GameObject lastSpawned;

    void Start()
    {
        // Initialize
        randomizer = GetComponent<ObjectRandomizer>();
        spawnedCanisters = -1;
        canSpawn = true;
        StartCoroutine(spawnAmmo());
    }

    // Coroutines
    IEnumerator spawnAmmo()
    {
        while (true)
        {
            // Spawn new canisters
            if (canSpawn)
            {
                Object canister = randomizer.RandomObject();
                Instantiate(canister, transform.position, Quaternion.identity, transform);
                spawnedCanisters += 1;
                lastSpawned = transform.GetChild(spawnedCanisters).gameObject;
            }

            // Make sure there is space to spawn a new canister
            if (lastSpawned!=null)
            {
                canSpawn = !lastSpawned.activeSelf;
                if (canSpawn)
                {
                    lastSpawned = null;
                    yield return new WaitForSeconds(UnityEngine.Random.Range(5, 20));
                }
            }

            yield return null;
        }
    }
}