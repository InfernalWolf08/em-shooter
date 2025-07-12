using System;
using System.Collections;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{
    [Header("Stats")]
    public float health=100;
    [Range(0.1f, 5f)] public float speed;
    private float baseSpeed;
    [Space][Space]
    [Header("Animation")]
    public Animator animator;
    public bool isMoving;
    private Vector3 lastPos;
    [Space][Space]
    [Header("AI")]
    [Header("Pathing")]
    public Transform player;
    public AIPath pathing;
    public AIDestinationSetter AiDest;
    public bool isChasing;
    [Header("Sight")]
    public Transform eyes;
    public Vector3 eyeSize = new Vector3(1, 1, 1);
    public LayerMask eyeMask;

    void Start()
    {
        // Initialize
        player = GameObject.FindWithTag("Player").transform;
        baseSpeed = speed;
        pathing.maxSpeed = speed;
        lastPos = transform.position;

        // Coroutines
        StartCoroutine(animate());
    }

    void Update()
    {
        // See the player
        RaycastHit2D hit = Physics2D.BoxCast(eyes.position, eyeSize, 0, eyes.right, eyeSize.z, eyeMask);
        if (hit)
        {
            if (hit.collider.gameObject.tag=="Player")
            {
                isChasing = true;
            }
        }

        // Hear the player


        // Chase the player
        if (isChasing)
        {
            // Flip towards player
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = player.transform.position.x<transform.position.x;
            
            // Chase
            AiDest.target = player;
        } else {
            // Stop Chasing
            AiDest.target = null;
        }
    }

    // Coroutines
    IEnumerator animate()
    {
        while (true)
        {
            // Animate the sprite
            isMoving = lastPos!=transform.position;
            lastPos = transform.position;
            animator.SetBool("isMoving", isMoving);
            
            yield return new WaitForSeconds(0.1f);
            yield return null;
        }
    }
}