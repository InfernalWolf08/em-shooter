using UnityEngine;

public class EndController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject asset;

    void Start()
    {
        // Initialize
        Instantiate(asset, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position, Quaternion.identity, transform.parent);
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        if (info.transform.parent.gameObject.tag=="Player")
        {
            info.transform.parent.gameObject.GetComponent<PlayerController>().win();
        }
    }
}