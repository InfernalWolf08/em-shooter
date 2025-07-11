using System;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{
    [Header("Stats")]
    public float health=100;
    [Range(0.1f, 10f)] public float speed;
    private float baseSpeed;

    [Header("AI")]
    public Transform player;
    public AIDestinationSetter AiDest;

    void Start()
    {
        // Initialize
        player = GameObject.FindWithTag("Player").transform;
        baseSpeed = speed;
        AiDest.target = player;
    }

    void Update()
    {

    }
}