using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monster;
    public bool canSpawn;
    private GameObject lastSpawned;
    private int spawnedMonsters = -1;

    void Start()
    {
        // Initialize
        StartCoroutine(spawnMonsters());
    }

    // Coroutines
    IEnumerator spawnMonsters()
    {
        while (true)
        {
            // Spawn new monsters
            if (canSpawn)
            {
                Instantiate(monster, transform.position, Quaternion.identity, transform);
                spawnedMonsters += 1;
                lastSpawned = transform.GetChild(spawnedMonsters).gameObject;
            }

            // Make sure there is space to spawn a new monster
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