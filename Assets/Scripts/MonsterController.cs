using System;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{
    [Header("Stats")]
    public float health=100;
    [Range(0.1f, 5f)] public float speed;
    private float baseSpeed;

    [Header("AI")]
    public Transform player;
    public AIPath pathing;
    public AIDestinationSetter AiDest;

    void Start()
    {
        // Initialize
        player = GameObject.FindWithTag("Player").transform;
        baseSpeed = speed;
        AiDest.target = player;
        pathing.maxSpeed = speed;
    }

    void Update()
    {
        // Flip towards player
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = player.transform.position.x<transform.position.x;
    }
}