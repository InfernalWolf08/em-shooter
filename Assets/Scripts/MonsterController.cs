using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("Stats")]
    public float health=100;
    [Range(0.1f, 10f)] public float speed;
    private float baseSpeed;

    [Header("AI")]
    public Transform player;

    void Start()
    {
        // Initialize
        player = GameObject.FindWithTag("Player").transform;
        baseSpeed = speed;
    }
}